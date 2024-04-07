///mini project stage3 roze arenbayev 329335202 and tal biton 329397970

using System.Reflection.Emit;
namespace BlTest;

using BlApi;
using BO;
using BL;

/// <summary>
/// the program in BO gets from user the data and calls DO to use it
/// </summary>
internal class Program
{
    static readonly IBl s_bl = BlApi.Factory.Get(); //stage 4

     public static BO.EngineerExperience SetEX(int num)//sets EngineerExperience (Enum) 
     {
        return num switch
        {
            0 => BO.EngineerExperience.Beginner,
            1 => BO.EngineerExperience.AdvancedBeginner,
            2 => BO.EngineerExperience.Intermediate,
            3 => BO.EngineerExperience.Advanced,
            4 => BO.EngineerExperience.Expert,
            _ => throw new Exception("no such level"),
        };
    }

     public static BO.Task CreateT()//creates a new task
     {
        bool flag;
         Console.WriteLine("enter task alias\n");
         string alias = Console.ReadLine();
         
        Console.WriteLine("enter task description\n");
         string description = Console.ReadLine();
         
        Console.WriteLine("enter task createdAtDate\n");
         flag = DateTime.TryParse(Console.ReadLine(), out DateTime createdAtDate);
         while (flag==false) 
         { 
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task createdAtDate\n");
            flag = DateTime.TryParse(Console.ReadLine(), out createdAtDate);
        }

        Console.WriteLine("enter tasks required Effort Time\n");
         flag = int.TryParse(Console.ReadLine(), out int days);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task days\n");
            flag = int.TryParse(Console.ReadLine(), out days);
        }
        Console.ReadLine();
        flag = int.TryParse(Console.ReadLine(), out int hours);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task hours\n");
            flag = int.TryParse(Console.ReadLine(), out hours);
        }
        Console.ReadLine();
        flag = int.TryParse(Console.ReadLine(), out int minuts);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task minuts\n");
            flag = int.TryParse(Console.ReadLine(), out minuts);
        }
        Console.ReadLine();
        flag = int.TryParse(Console.ReadLine(), out int sec);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task sec\n");
            flag = int.TryParse(Console.ReadLine(), out sec);
        }
        TimeSpan requiredEffortTime = new TimeSpan(days, hours, minuts, sec);
         
        Console.WriteLine("enter task complexity\n");
         BO.EngineerExperience complexity = SetEX(int.Parse(Console.ReadLine()));

        Console.WriteLine("enter task deliverables\n");
        string deliverables = Console.ReadLine();

        Console.WriteLine("enter engineer Id\n");
        flag = int.TryParse(Console.ReadLine(), out int engineerId);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task engineerId\n");
            flag = int.TryParse(Console.ReadLine(), out engineerId);
        }

        Console.WriteLine("enter tasks remarks\n");
         string? remarks = Console.ReadLine();

        Console.WriteLine("enter tasks scheduledDate\n");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime scheduledDate);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task scheduledDate\n");
            flag = DateTime.TryParse(Console.ReadLine(), out scheduledDate);
        }

        Console.WriteLine("enter tasks completeDate\n");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime completeDate);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task completeDate\n");
            flag = DateTime.TryParse(Console.ReadLine(), out completeDate);
        }

        Console.WriteLine("enter tasks deadLineDate\n");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime deadLineDate);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task deadLineDate\n");
            flag = DateTime.TryParse(Console.ReadLine(), out deadLineDate);
        }

        Console.WriteLine("enter task startDate\n");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime startDate);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task startDate\n");
            flag = DateTime.TryParse(Console.ReadLine(), out startDate);
        }
        BO.Task item = new BO.Task
        {
            Alias = alias,
            Description= description,
            CreatedAtDate= createdAtDate,
            RequiredEffortTime= requiredEffortTime,
            Complexity= complexity,
            Deliverables= deliverables,
            EngineerId= engineerId,
            Remarks= remarks,
            ScheduledDate = scheduledDate,
            CompleteDate= completeDate,
            DeadLineDate= deadLineDate,
            StartDate= startDate
        };
        
         s_bl.Task.Create(item);
         return item;
     }
     public static BO.Engineer CreateE()//creates a new engineer 
     {
        bool flag;
         Console.WriteLine("enter engineer id\n");
        flag = int.TryParse(Console.ReadLine(), out int id);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter engineer id\n");
            flag = int.TryParse(Console.ReadLine(), out id);
        }

        Console.WriteLine("enter engineer name\n");
         string name = Console.ReadLine();

         Console.WriteLine("enter engineer email\n");
         string email = Console.ReadLine();

         Console.WriteLine("enter engineer level\n");
         BO.EngineerExperience level = SetEX(Convert.ToInt32(Console.ReadKey()));

         Console.WriteLine("enter engineer cost for hour\n");
        flag = double.TryParse(Console.ReadLine(), out double cost);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter engineer cost\n");
            flag = double.TryParse(Console.ReadLine(), out cost);
        }

        BO.Engineer item = new BO.Engineer()
        {
            Id = id,
            Name = name,
            Email = email,
            Cost = cost,

        };
        s_bl.Engineer.Create(item);
        return item;
     }

    public static void TaskImp()//task menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : print a specific task \n 4: print all tasks \n 5: update a task\n");
        int.TryParse(Console.ReadLine(), out int choice);
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:///create a new task
                    {
                        try
                        {
                            BO.Task newT = CreateT();
                            Console.WriteLine(newT.ToString()); ;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                    }
                case 2:///delete a task
                    {
                        try
                        {
                            int.TryParse(Console.ReadLine(), out int id2);
                            s_bl.Task.Delete(id2);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                    }
                case 3:///find a specific task
                    {
                        try
                        {
                            int.TryParse(Console.ReadLine(), out int id);
                            Console.Write(s_bl.Task.Read(id));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                    }
                case 4:///find all tasks
                    try
                    {
                        Console.Write(s_bl.Task.ReadAll());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
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
                default: throw new BlNotAPossabilityException("no such possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : find a specific task \n 4: find all tasks \n 5: update a task\n");
            int.TryParse(Console.ReadLine(), out  choice);
        }
    }
    public static void EngineerImp()//engineer menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new engineer \n 2 : delete a engineer \n 3 : find a specific engineer \n 4: find all engineers \n 5: update an engineer\n");
        int.TryParse(Console.ReadLine(), out int choice);
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:///create a new engineer
                    try
                    {
                        BO.Engineer newE = CreateE();
                        Console.Write(newE.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 2:///delete a engineer
                    try
                    {
                        int.TryParse(Console.ReadLine(), out int id);
                        s_bl.Engineer.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:///find a specific engineer
                    int.TryParse(Console.ReadLine(), out int id2);
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
                default: throw new Exception("no such possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new engineer \n 2 : delete a engineer \n 3 : find a specific engineer \n 4: find all engineers \n 5: update an engineer\n");
            int.TryParse(Console.ReadLine(), out choice);
        }
    }

    static void MainMenu()//menu
    {
        Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation\n");
        int.TryParse(Console.ReadLine(), out int choice);
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
                default: throw new Exception("no such possibility");
            }
            Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation\n");
            int.TryParse(Console.ReadLine(), out choice);
        }

    }

    static void Main(string[] args)
    {
        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();
            MainMenu();

        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }


    }


}


