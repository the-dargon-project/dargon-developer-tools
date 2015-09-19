using Dargon.League.FileExplorer.Controllers;
using Dargon.League.FileExplorer.ViewModels;
using Dargon.Ryu;

namespace Dargon.League.FileExplorer {
   public class FileExplorerRyuPackage : RyuPackageV1 {
      public FileExplorerRyuPackage() {
         Singleton<NodeOperationsController>();
         Singleton<NodeViewModelFactory>();
      }
   }
}
