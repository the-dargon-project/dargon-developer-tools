using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.RADS;
using Dargon.RADS.Archives;

namespace Dargon.League.FileExplorer.Controllers.Helpers {
   public class ArchiveCache {
      private readonly Dictionary<uint, IReadOnlyList<RiotArchive>> archivesById = new Dictionary<uint, IReadOnlyList<RiotArchive>>();


      public void Initialize() {
         var projectLoader = new RiotProjectLoader(@"T:\Games\LeagueOfLegends\RADS");
         var gameProject = projectLoader.LoadProject(RiotProjectType.GameClient);

         var gameArchiveIds = gameProject.ReleaseManifest.Files.Select(x => x.ArchiveId).Distinct().ToArray();

         var archiveLoader = new RiotArchiveLoader(@"T:\Games\LeagueOfLegends\RADS");
         foreach (var archiveId in gameArchiveIds) {
            IReadOnlyList<RiotArchive> archives;
            if (!archiveLoader.TryLoadArchives(archiveId, out archives)) {
               Console.WriteLine($"Failed to load archive: {archiveId}");
            } else {
               archivesById.Add(archiveId, archives);
            }
         }
      }

      public IReadOnlyList<RiotArchive> GetArchive(uint id) => archivesById[id];
   }
}
