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
/// a window to add or update an engineer
/// </summary>
/// <parameter name="Eng">: the engineer that i get from the engineer window </parameter>
/// <method name="AddEngineer">: constractor of the window </method>
/// <method name="BtnAddUpdate_Click">: the button of the add or the update </method>

public partial class AddEngineer : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    private BO.Engineer? Eng;
    bool add = false;
    public AddEngineer(int id = 0)
    {
        InitializeComponent();
        try
        {
            if (id == 0)//in case of add
            {
                Eng = new BO.Engineer()
                {
                    Id = id,
                    Name = "rose",
                    Email = "tit@gmail.com",
                    Level = BO.EngineerExperience.Beginner,
                    Cost = 39.5,
                    Task = null//adding the current task that the engineer is workng on
                };
                add = true;
            }

            else//in case of update
            {
                Eng = s_bl?.Engineer.Read(id)!;
                add = false;
            }
             
        }
        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (add==true)//in case of add
            {
                s_bl.Engineer.Create(Eng);
                MessageBox.Show("engineer was succsesfuly created");
            }
            else//in case of update
            {
                s_bl.Engineer.Update(Eng);
                MessageBox.Show("engineer was succsesfuly updated");
            }
        }

        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlCantBeUpdetedException ex) { MessageBox.Show(ex.Message); }
        catch(Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void Button_Click_Add(object sender, RoutedEventArgs e)
    {
        try
        {
            if (add == true)//in case of add
            {
                s_bl.Engineer.Create(Eng);
                MessageBox.Show("engineer was succsesfuly created");
            }
            else//in case of update
            {
                MessageBox.Show("You do not have permission for this action");
            }
        }

        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlCantBeUpdetedException ex) { MessageBox.Show(ex.Message); }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void Button_Click_Update(object sender, RoutedEventArgs e)
    {
        try
        {
            if (add == true)//in case of add
            {
                MessageBox.Show("You do not have permission for this action");
            }
            else//in case of update
            {
                s_bl.Engineer.Update(Eng);
                MessageBox.Show("engineer was succsesfuly updated");
            }
        }

        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
        catch (BO.BlCantBeUpdetedException ex) { MessageBox.Show(ex.Message); }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
}