using System.Windows;
using System.Windows.Controls;

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
/// <method name="EngineerWindow">: constractor of the window </method>
/// <parameter name="EngList">: a list of all the enginners </parameter>
/// <method name="ComboBox_SelectionChanged">: select all the engineers with a specipic experiance </method>
/// <method name="ButtonAddEngineer_Click">: open the window of add engineer and addes a engineer</method>
/// <method name="ListView_OpenEngineer">: open the window of add engineer and updet an engineer</method>

public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.Level;

    public static readonly DependencyProperty EngListProperty =
        DependencyProperty.Register(nameof(EngList), typeof(IEnumerable<BO.Engineer>), typeof(EngineerWindow));
    public IEnumerable<BO.Engineer>? EngList
    {
        get => (IEnumerable<BO.Engineer>)GetValue(EngListProperty);
        set => SetValue(EngListProperty, value);
    }

    public EngineerWindow()
    {
        InitializeComponent();
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        EngList = (Experience != BO.EngineerExperience.Level) ?
           s_bl?.Engineer.ReadAllEngineers() : s_bl?.Engineer.ReadAllEngineers(item => item.Level == Experience);
    }

    private void ButtonAddEngineer_Click(object sender, RoutedEventArgs e)
    {
        new AddEngineer().ShowDialog();
        EngList = s_bl?.Engineer.ReadAllEngineers();
    }
    
    private void ListView_OpenEngineer(object sender, RoutedEventArgs e)
    {
        BO.Engineer? eng = (sender as ListView)?.SelectedItem as BO.Engineer;
        new AddEngineer(eng.Id).ShowDialog();
        EngList = s_bl?.Engineer.ReadAllEngineers();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EngList = s_bl?.Engineer.ReadAllEngineers();
    }
}

