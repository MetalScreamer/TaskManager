using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Jsc.Wpf;

namespace Jsc.TaskManager
{
    /// <summary>
    /// Interaction logic for JobListView.xaml
    /// </summary>
    public partial class JobListView : UserControl
    {
        public JobListView()
        {
            InitializeComponent();

            dataGrid.DisableOffRowContextMenu();
        }

        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var cell = GetCell(e);

            if (cell == null)
            {
                var dataGrid = (sender as DataGrid);
                dataGrid.SelectedItem = null;
            }
        }

        private void DataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var cell = GetCell(e);

            if (cell != null)
            {
                (sender as DataGrid).SelectedItem = cell.DataContext;
            }
        }

        private static DataGridCell GetCell(MouseButtonEventArgs e)
        {
            var dependencyObject = e.OriginalSource as DependencyObject;
            while (dependencyObject != null && dependencyObject.GetType() != typeof(DataGridCell))
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);

            return dependencyObject as DataGridCell;
        }
    }
}
