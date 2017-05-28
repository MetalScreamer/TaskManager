using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Jsc.Wpf
{
    public class DataGrid : System.Windows.Controls.DataGrid
    {
        public static readonly DependencyProperty DisableOffRowContextMenuProperty =
            DependencyProperty.Register(
                nameof(DisableOffRowContextMenu),
                typeof(bool),
                typeof(DataGrid),
                new UIPropertyMetadata(false, new PropertyChangedCallback(DisableOffRowContextMenuChanged)));        

        public static readonly DependencyProperty DisableOffRowDoubleClickProperty =
            DependencyProperty.Register(
                nameof(DisableOffRowDoubleClick),
                typeof(bool),
                typeof(DataGrid),
                new UIPropertyMetadata(false, new PropertyChangedCallback(DisableOffRowDoubleClickChanged)));        

        public static readonly DependencyProperty DeselectWhenClickOffRowProperty =
            DependencyProperty.Register(
                nameof(DeselectWhenClickOffRow),
                typeof(bool),
                typeof(DataGrid),
                new UIPropertyMetadata(false, DeselectWhenClickOffRowChanged));        

        public bool DisableOffRowContextMenu
        {
            get
            {
                return (bool)GetValue(DisableOffRowContextMenuProperty);
            }
            set
            {
                SetValue(DisableOffRowContextMenuProperty, value);
            }
        }

        public bool DeselectWhenClickOffRow
        {
            get
            {
                return (bool)GetValue(DeselectWhenClickOffRowProperty);
            }
            set
            {
                SetValue(DeselectWhenClickOffRowProperty, value);
            }
        }

        public bool DisableOffRowDoubleClick
        {
            get
            {
                return (bool)GetValue(DisableOffRowDoubleClickProperty);
            }
            set
            {
                SetValue(DisableOffRowDoubleClickProperty, value);
            }
        }

        private static void DisableOffRowDoubleClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid != null)
            {
                if ((bool)e.NewValue)
                {
                    dataGrid.MouseDoubleClick += DataGrid_MouseDoubleClick;               
                }
                else
                {
                    dataGrid.MouseDoubleClick -= DataGrid_MouseDoubleClick;
                }
            }
        }

        private static void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            bool isCell = IsCell((DependencyObject)e.OriginalSource);
            if (!isCell)
            {
                e.Handled = true;
            }
        }

        private static void DisableOffRowContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid != null)
            {
                if ((bool)e.NewValue)
                {
                    dataGrid.ContextMenuOpening += DataGrid_ContextMenuOpening;
                }
                else
                {
                    dataGrid.ContextMenuOpening -= DataGrid_ContextMenuOpening;
                }
            }
        }

        private static void DataGrid_ContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            bool isCell = IsCell((DependencyObject)e.OriginalSource);
            if (!isCell)
            {
                e.Handled = true;
            }
        }

        private static void DeselectWhenClickOffRowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid != null)
            {
                if ((bool)e.NewValue)
                {
                    dataGrid.MouseDown += DataGrid_MouseDown;
                    dataGrid.MouseRightButtonDown += DataGrid_MouseRightButtonDown;
                }
                else
                {
                    dataGrid.MouseDown -= DataGrid_MouseDown;
                    dataGrid.MouseRightButtonDown -= DataGrid_MouseRightButtonDown;
                }
            }
        }

        private static void DataGrid_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var row = GetControl<System.Windows.Controls.DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null && sender is DataGrid)
            {
                var dataGrid = sender as DataGrid;

                dataGrid.SelectedIndex = row.GetIndex();
            }
        }

        private static void DataGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var cell = GetControl<System.Windows.Controls.DataGridCell>((DependencyObject)e.OriginalSource);

            if (cell == null && sender is DataGrid)
            {
                var dataGrid = sender as DataGrid;
                dataGrid.SelectedIndex = -1;
            }
        }        

        private static bool IsCell(DependencyObject dependencyObject)
        {
            var cell = GetControl<System.Windows.Controls.DataGridCell>(dependencyObject);

            return cell != null;
        }

        private static T GetControl<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            while (dependencyObject != null && dependencyObject.GetType() != typeof(T))
            {
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            return dependencyObject as T;
        }
    }
}