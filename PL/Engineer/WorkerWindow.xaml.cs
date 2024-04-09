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
    int id = 0;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public WorkerWindow(int getid = 0)
    {
        InitializeComponent();
        int id = getid;
        BO.Engineer eng=s_bl.Engineer.Read(id);
        if(eng != null)
        {
            if (eng.Task != null)
                new TaskWindow(eng.Task.Id, true).Show();
        }
        else
        {
            MessageBox.Show("Engineer with this id was not found \n");
        }

       // BO.TaskInEngineer MyTask = s_bl?.Engineer.Read(id).Task;

    }

    private void ButtonTaskForList_Click(object sender, RoutedEventArgs e)
    {
        new TaskForListWindow(id).Show();//סינון המשימות
    }


}
