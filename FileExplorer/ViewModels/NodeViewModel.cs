using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;
using Dargon.Client.ViewModels.Helpers;
using Dargon.IO;
using Dargon.League.FileExplorer.Controllers;

namespace Dargon.League.FileExplorer.ViewModels {
   public class NodeViewModel {
      private readonly NodeOperationsController nodeOperationsController;
      private readonly IReadableDargonNode node;
      private readonly IReadOnlyList<NodeViewModel> children;
         
      public NodeViewModel(NodeOperationsController nodeOperationsController, IReadableDargonNode node, IReadOnlyList<NodeViewModel> children) {
         this.nodeOperationsController = nodeOperationsController;
         this.node = node;
         this.children = children;
      }

      public string Name => node.Name;
      public IReadOnlyList<NodeViewModel> Children => children;
      public bool IsDirectory => children.Any();

      public ICommand DumpCommand => new ActionCommand(x => {
         nodeOperationsController.DumpNode(node);
      });
   }

   public class NodeViewModelFactory {
      private readonly NodeOperationsController nodeOperationsController;

      public NodeViewModelFactory(NodeOperationsController nodeOperationsController) {
         this.nodeOperationsController = nodeOperationsController;
      }

      public NodeViewModel CreateFromNode(IReadableDargonNode node) {
         var children = node.Children.Select(CreateFromNode).ToArray();
         return new NodeViewModel(nodeOperationsController, node, children);
      }
   }
}
