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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        public EngineerListWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
/*<ListBox x:Name="list" HorizontalAlignment="Stretch" Grid.Row="1" VerticalAlignment="Stretch"
                     SelectionChanged="lbFromXML_SelectionChanged">
              <ListBox.Items>
                <ListBoxItem Content="beginner"/>
                <ListBoxItem Content="advanced beginer"/>
                <ListBoxItem Content="intermediate"/>
                <ListBoxItem Content="advancrd"/>
                <ListBoxItem Content="expert"/>
                </ListBox.Items>     
            */
