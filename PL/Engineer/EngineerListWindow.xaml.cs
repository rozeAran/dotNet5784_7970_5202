using System.Windows;
using System.Windows.Controls;

namespace PL.Engineer;

/// <summary>
/// a window that shos you the list of all the engineers 
/// </summary>
/// <method name="EngineerListWindow">: constractor of the window </method>
/// <parameter name="EngList">: a list of all the enginners </parameter>
/// <method name="ComboBox_SelectionChanged">: select all the engineers with a specipic experiance </method>
/// <method name="ButtonAddEngineer_Click">: open the window of add engineer and addes a engineer</method>
/// <method name="ListView_OpenEngineer">: open the window of add engineer and updet an engineer</method>

public partial class EngineerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.Level;

    public static readonly DependencyProperty EngListProperty =
        DependencyProperty.Register(nameof(EngList), typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow));
    public IEnumerable<BO.Engineer>? EngList
    {
        get => (IEnumerable<BO.Engineer>)GetValue(EngListProperty);
        set => SetValue(EngListProperty, value);
    }

    public EngineerListWindow()
    {
        InitializeComponent();
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        EngList = (Experience != BO.EngineerExperience.Level) ?
            s_bl?.Engineer.ReadAllEngineers(item => item.Level == Experience): s_bl?.Engineer.ReadAllEngineers() ;
    }

    private void ButtonAddEngineer_Click(object sender, RoutedEventArgs e)
    {
        new AddOrUpdateEngineer().ShowDialog();
        EngList = s_bl?.Engineer.ReadAllEngineers();
    }
    
    private void ListView_OpenEngineer(object sender, RoutedEventArgs e)
    {
        BO.Engineer? eng = (sender as ListView)?.SelectedItem as BO.Engineer;
        new AddOrUpdateEngineer(eng.Id).ShowDialog();
        EngList = s_bl?.Engineer.ReadAllEngineers();
    }

    /*private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EngList = s_bl?.Engineer.ReadAllEngineers();
    }*/
}
