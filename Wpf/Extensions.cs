using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jsc.Wpf
{
    public static class Extensions
    {
        public static void DisableOffRowContextMenu(this DataGrid dataGrid)
        {
            dataGrid.ContextMenuOpening += DataGrid_ContextMenuOpening;
        }

        private static void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var dependencyObject = e.OriginalSource as DependencyObject;
            while (dependencyObject != null && dependencyObject.GetType() != typeof(DataGridRow))
            {
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            if (dependencyObject == null)
            {
                e.Handled = true;
            }
        }
    }
}
