using PL.Task;
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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for WorkerWindow.xaml
/// </summary>
public partial class WorkerWindow : Window
{
    public WorkerWindow()
    {
        id = getId;
        InitializeComponent();

    }

    private void ButtonTaskForList_Click(object sender, RoutedEventArgs e)
    {
        new TaskForListWindow().Show();//סינון המשימות
    }

    private string LabelContent(object sender, RoutedEventArgs e) 
    {
        Task t=id.task;//הצגה של המשימה הנוכחית של העובד
       
    }

}
