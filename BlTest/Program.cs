///mini project stage3 roze arenbayev 329335202 and tal biton 329397970

using System.Reflection.Emit;
namespace BlTest;

using BlApi;
using BO;
using BL;


internal class Program
{
    static readonly IBl s_bl = BlApi.Factory.Get(); //stage 4

    /* public static BO.EngineerExperience SetEX(int num)//sets EngineerExperience (Enum) 
     {
         switch (num)
         {
             case 0: return BO.EngineerExperience.Beginner;
             case 1: return BO.EngineerExperience.AdvancedBeginner;
             case 2: return BO.EngineerExperience.Intermediate;
             case 3: return BO.EngineerExperience.Advanced;
             case 4: return BO.EngineerExperience.Expert;
             default:
                 throw new Exception("no such level");
         }
     }

     public static BO.Task CreateT()//creates a new task
     {
         Console.WriteLine("enter task alias\n");
         string alias = Console.ReadLine();
         Console.WriteLine("enter task description\n");
         string description = Console.ReadLine();
         Console.WriteLine("enter task createdAtDate\n");
         DateTime createdAtDate = DateTime.Parse(Console.ReadLine());
         Console.WriteLine("enter tasks required Effort Time\n");
         int days = int.Parse(Console.ReadLine());
         Console.ReadKey();
         int hours = int.Parse(Console.ReadLine());
         Console.ReadKey();
         int sec = int.Parse(Console.ReadLine());
         Console.ReadKey();
         int minutes = int.Parse(Console.ReadLine());
         TimeSpan requiredEffortTime = new TimeSpan(days, hours, sec, minutes);
         Console.WriteLine("enter task complexity\n");
         int temp = int.Parse(Console.ReadLine());
         BO.EngineerExperience complexity = SetEX(int.Parse(Console.ReadLine()));
         Console.WriteLine("enter task deliverables\n");
         string deliverables = Console.ReadLine();
         Console.WriteLine("enter engineer Id\n");
         int engineerId = Console.Read();
         Console.WriteLine("enter tasks remarks\n");
         string? remarks = Console.ReadLine();
         Console.WriteLine("enter tasks scheduledDate\n");
         DateTime? scheduledDate = DateTime.Parse(Console.ReadLine());
         Console.WriteLine("enter tasks completeDate\n");
         DateTime? completeDate = DateTime.Parse(Console.ReadLine());
         Console.WriteLine("enter tasks deadLineDate\n");
         DateTime? deadLineDate = DateTime.Parse(Console.ReadLine());
         Console.WriteLine("enter if the task is a mile stone\n");
         bool isMilestone = bool.Parse(Console.ReadLine());
         Console.WriteLine("enter task startDate\n");
         DateTime? startDate = DateTime.Parse(Console.ReadLine());
         BO.Task item = new BO.Task();
         return item;
     }
     public static BO.Engineer CreateE()//creates a new engineer 
     {
         Console.WriteLine("enter engineer id\n");
         int id = int.Parse(Console.ReadLine());
         Console.WriteLine("enter engineer name\n");
         string name = Console.ReadLine();
         Console.WriteLine("enter engineer email\n");
         string email = Console.ReadLine();
         Console.WriteLine("enter engineer level\n");
         BO.EngineerExperience level = SetEX(Convert.ToInt32(Console.ReadKey()));
         Console.WriteLine("enter engineer cost for hour\n");
         double cost = double.Parse(Console.ReadLine());
         BO.Engineer item = new BO.Engineer();
         return item;
     }*/

    public static void TaskImp()//task menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : print a specific task \n 4: print all tasks \n 5: update a task\n");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:///create a new task
                    Console.Write(s_bl.Task.Create(DalTest.(program.cs).CreateT()));
                    break;
                case 2:///delete a task
                    try
                    {
                        int id2 = int.Parse(Console.ReadLine());
                        s_bl.Task.Delete(id2);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:///find a specific task
                    int id = int.Parse(Console.ReadLine());
                    Console.Write(s_bl.Task.Read(id));
                    break;
                case 4:///find all tasks
                    Console.Write(s_bl.Task.ReadAll());
                    break;
                case 5:///update a task
                    try
                    {
                        Console.Write(s_bl.Task.ReadAll());
                        Console.WriteLine("please enter the  task's updated values");
                        BO.Task temp = CreateT();
                        if (temp.ScheduledDate != null)
                            s_bl.Task.Update(temp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default: throw new BlNotAPossabilityException("no suche possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : find a specific task \n 4: find all tasks \n 5: update a task\n");
            choice = int.Parse(Console.ReadLine());
        }
    }
    public static void EngineerImp()//engineer menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new engineer \n 2 : delete a engineer \n 3 : find a specific engineer \n 4: find all engineers \n 5: update an engineer\n");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:///create a new engineer
                    try
                    {
                        Console.Write(s_bl.Engineer.Create(CreateE()));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 2:///delete a engineer

                    try
                    {
                        int id = Convert.ToInt32(Console.ReadKey());
                        s_bl.Engineer.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:///find a specific engineer
                    int id2 = Convert.ToInt32(Console.ReadKey());
                    Console.Write(s_bl.Engineer.Read(id2));
                    break;
                case 4:///find all engineers
                    Console.Write(s_bl.Engineer.ReadAll());
                    break;
                case 5:///update an engineers
                    try
                    {
                        Console.Write(s_bl.Engineer.ReadAll());
                        Console.WriteLine("please enter the dependency's updated values");
                        BO.Engineer temp = CreateE();
                        if (temp.Id != 0)
                            s_bl.Engineer.Update(temp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default: throw new Exception("no suche possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new engineer \n 2 : delete a engineer \n 3 : find a specific engineer \n 4: find all engineers \n 5: update an engineer\n");
            choice = Convert.ToInt32(Console.ReadKey());
        }
    }

    static void MainMenu()//menu
    {
        Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation\n 4 : Initialization\n");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    {
                        TaskImp();
                        break;
                    }
                case 2:
                    {
                        EngineerImp();
                        break;
                    }
                case 3:
                    {
                        Console.Write("Would you like to create Initial data? (Y/N)");
                        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
                        if (ans == "Y")
                            DalTest.Initialization.Do();
                        break;

                    }
                default: throw new Exception("no such possibility");
            }
            Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation\n");
            choice = int.Parse(Console.ReadLine());
        }

    }

    static void Main(string[] args)
    {
        try
        {
            MainMenu();

        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }


    }


}


