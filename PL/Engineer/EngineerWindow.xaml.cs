using BO;
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
/// Interaction logic for EngineerWindow.xaml
/// </summary>

public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.Beginner;
    public EngineerWindow()
    {
        InitializeComponent();
        EngList = s_bl?.Engineer.ReadAll()!;
    }
    public IEnumerable<BO.EngineerInTask> EngList
    {
        get { return (IEnumerable<BO.EngineerInTask>)GetValue(EngListProperty); }
        set { SetValue(EngListProperty, value); }
    }

    public static readonly DependencyProperty EngListProperty =
        DependencyProperty.Register("CourseList", typeof(IEnumerable<BO.EngineerInTask>), typeof(EngineerWindow), new PropertyMetadata(null));

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        EngList = s_bl.Engineer.ReadAll();
    }

    private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
     {
        
        EngList = (Experience == BO.EngineerExperience.Beginner) ?
           s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == Experience)!;
    }

    private void ButtonAddEngineer_Click(object sender, RoutedEventArgs e)
    {
        new AddEngineer().ShowDialog();
    }
    
    private void ListView_OpenEngineer(object sender, RoutedEventArgs e)
    {
        BO.Engineer? eng = (sender as ListView)?.SelectedItem as BO.Engineer;

        new AddEngineer(eng.Id).ShowDialog();
    }


}
