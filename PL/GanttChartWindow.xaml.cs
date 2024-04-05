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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for GanttChartWindow.xaml
    /// </summary>
    public partial class GanttChartWindow : Window
    {
        static readonly BlApi.IBl? s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.Task?> TaskListingGantt
        {
            get { return (IEnumerable<BO.Task?>)GetValue(TaskListingGanttProperty); }
            set { SetValue(TaskListingGanttProperty, value); }
        }

        
        public static readonly DependencyProperty TaskListingGanttProperty =
            DependencyProperty.Register("TaskListingGantt", typeof(IEnumerable<BO.Task?>), typeof(GanttChartWindow), new PropertyMetadata(null));
        public GanttChartWindow()
        {
            TaskListingGantt = s_bl?.Task.ReadAll()!;
            TaskListingGantt = TaskListingGantt.OrderBy(Task => Task.StartDate);
            InitializeComponent();
        }
    }
}
