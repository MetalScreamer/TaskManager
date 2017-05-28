using Jsc.Wpf;
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

namespace Jsc.TaskManager
{
    /// <summary>
    /// Interaction logic for TaskList.xaml
    /// </summary>
    public partial class TaskListView : UserControl
    {
        public TaskListView()
        {
            InitializeComponent();
            //dataGrid.DisableOffRowContextMenu();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = e.OriginalSource as DependencyObject;

            while (dependencyObject != null && dependencyObject.GetType() != typeof(DataGridCell))
            {
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            if(dependencyObject == null)
            {
                e.Handled = true;
            }
        }
    }
}
