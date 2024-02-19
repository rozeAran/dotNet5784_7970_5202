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
/// Interaction logic for AddEngineer.xaml
/// </summary>
public partial class AddEngineer : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    BO.Engineer? eng;
    public AddEngineer(int id=0)
    {
        InitializeComponent();
        try
        {
            if (id == 0)
                eng = new BO.Engineer()
                {
                    Id = id,
                    Name = "rose",
                    Email = "tit@gmail.com",
                    Level = BO.EngineerExperience.Beginner,
                    Cost = 39.5,
                    Tasks = null//adding the current task that the engineer is workng on
                };
            else
                eng = s_bl?.Engineer.Read(id)!;
        }
        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
    }

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (eng.Id == 0)
            {
                s_bl.Engineer.Create(eng);
                MessageBox.Show("engineer was succsesfuly created");
            }
            else
            {
                s_bl.Engineer.Update(eng);
                MessageBox.Show("engineer was succsesfuly updated");
            }
        }
        catch  (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
        catch(BO.BlCantBeUpdetedException ex) { MessageBox.Show(ex.Message); }
    }

}
