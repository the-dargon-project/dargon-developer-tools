using System.IO;
using System.Linq;
using Dargon.IO;
using Dargon.League.FileExplorer.Controllers.Helpers;
using Dargon.RADS.Manifest;
using ItzWarty.IO;

namespace Dargon.League.FileExplorer.Controllers {
   public class NodeOperationsController {
      private readonly IFileSystemProxy fileSystemProxy;
      private readonly ArchiveCache archiveCache;

      public NodeOperationsController(IFileSystemProxy fileSystemProxy, ArchiveCache archiveCache) {
         this.fileSystemProxy = fileSystemProxy;
         this.archiveCache = archiveCache;
      }

      public void DumpNode(IReadableDargonNode node) {
         var leaves = node.EnumerateLeaves().Cast<ReleaseManifestFileEntry>();

         foreach (var leaf in leaves) {
            var archives = archiveCache.GetArchive(leaf.ArchiveId);
            var entry = archives.Select(archive => new { archive, entry = archive.GetDirectoryFile().GetFileList().GetFileEntryOrNull(leaf.GetPath()) })
                                .First(pair => pair.entry != null).entry;
            var content = entry.GetContent();

            var outputPath = @"C:/dargonDump" + leaf.GetPath();
            fileSystemProxy.PrepareParentDirectory(outputPath);
            File.WriteAllBytes(outputPath, content);
         }
      }
   }
}
