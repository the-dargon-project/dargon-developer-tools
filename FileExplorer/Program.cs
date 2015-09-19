using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Dargon.IO;
using Dargon.IO.Resolution;
using Dargon.League.FileExplorer.Controllers;
using Dargon.League.FileExplorer.ViewModels;
using Dargon.RADS;
using Dargon.RADS.Archives;
using Dargon.RADS.Manifest;
using Dargon.Ryu;
using ItzWarty;

namespace Dargon.League.FileExplorer {
   public class Program {
      [STAThread]
      public static void Main() {
         var ryu = new RyuFactory().Create();
         ryu.Touch<ItzWartyCommonsRyuPackage>();
         ryu.Touch<ItzWartyProxiesRyuPackage>();
         ryu.Setup();

         var application = Application.Current ?? new Application();

         var projectLoader = new RiotProjectLoader(@"T:\Games\LeagueOfLegends\RADS");
         var gameProject = projectLoader.LoadProject(RiotProjectType.GameClient);

         var nodeViewModelFactory = ryu.Get<NodeViewModelFactory>();
         var viewModel = nodeViewModelFactory.CreateFromNode(gameProject.ReleaseManifest.Root);

         var window = new FileExplorer {
            DataContext = new[] { viewModel }
         };
         application.Run(window);
      }
   }
}
