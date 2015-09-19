using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Dargon.League.FileExplorer.Views.Helpers {
   public class TreeViewSorter : IValueConverter {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
         var collection = value as System.Collections.IList;
         var view = new ListCollectionView(collection);
         view.SortDescriptions.Add(new SortDescription("IsDirectory", ListSortDirection.Descending));
         view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));


         return view;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
         throw null;
      }
   }
}
