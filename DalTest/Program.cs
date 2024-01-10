///mini project stage1 roze arenbayev 329335202 and tal biton 329397970

using System.Reflection.Emit;
namespace DalTest;
using Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;



internal class Program
{

    //private static ITask? s_dalTask = new TaskImplementation();
    //private static IEngineer? s_dalEngineer = new EngineerImplementation();
    //private static IDependency? s_dalDependency = new DependencyImplementation();
    static readonly IDal s_dal = new DalList(); //stage 2


    public static EngineerExperience SetEX(int num)//sets EngineerExperience (Enum) 
    {
        switch(num) 
        {
            case 0: return EngineerExperience.Beginner;
            case 1: return EngineerExperience.AdvancedBeginner;
            case 2: return EngineerExperience.Intermediate;
            case 3: return EngineerExperience.Advanced;
            case 4: return EngineerExperience.Expert;
            default:
                throw new Exception("no such level");
        }
    }

    public static Task CreateT()//creates a new task
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
        int hours= int.Parse(Console.ReadLine());
        Console.ReadKey();
        int sec = int.Parse(Console.ReadLine());
        Console.ReadKey();
        int minutes = int.Parse(Console.ReadLine());
        TimeSpan requiredEffortTime= new TimeSpan(days,hours,sec,minutes);
        Console.WriteLine("enter task complexity\n");
        int temp= int.Parse(Console.ReadLine());
        EngineerExperience complexity=SetEX(int.Parse(Console.ReadLine()));
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
        
        DO.Task item = new DO.Task(0, alias, description, createdAtDate, requiredEffortTime, complexity, deliverables, engineerId, remarks, scheduledDate, completeDate, deadLineDate, isMilestone, startDate);
        return item;
    }
    public static Dependency CreateD()//creates a new dependency
    {
        Console.WriteLine("enter dependentTask \n ");
        ///int id= int.Parse(Console.ReadLine());
        int dependentTask= int.Parse(Console.ReadLine());
        Console.WriteLine("enter dependOnTask \n ");
        int dependOnTask= int.Parse(Console.ReadLine());
        DO.Dependency item = new DO.Dependency(0, dependentTask, dependOnTask);
        return item;
    }
    public static Engineer CreateE()//creates a new engineer 
    {
        Console.WriteLine("enter engineer id\n");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("enter engineer name\n");
        string name = Console.ReadLine();
        Console.WriteLine("enter engineer email\n");
        string email = Console.ReadLine();
        Console.WriteLine("enter engineer level\n");
        EngineerExperience level = SetEX(Convert.ToInt32(Console.ReadKey()));
        Console.WriteLine("enter engineer cost for hour\n");
        double cost = double.Parse(Console.ReadLine());
        DO.Engineer item = new DO.Engineer(id, name, email, level, cost);
        return item;
    }

    public static void TaskImp()//task menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : print a specific task \n 4: print all tasks \n 5: update a task\n");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:///create a new task
                    Console.Write(s_dal.Task.Create(CreateT()));
                    break;
                case 2:///delete a task
                    try
                    {
                        int id2 = int.Parse(Console.ReadLine());
                        s_dal.Task.Delete(id2);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:///find a specific task
                    int id = int.Parse(Console.ReadLine());
                    Console.Write(s_dal.Task.Read(id));
                    break;
                case 4:///find all tasks
                    Console.Write(s_dal.Task.ReadAll());
                    break;
                case 5:///update a task
                    try
                    {
                        Console.Write(s_dal.Task.ReadAll());
                        Console.WriteLine("please enter the  task's updated values");
                        DO.Task temp = CreateT();
                        if(temp.ScheduledDate != null)
                          s_dal.Task.Update(temp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default: throw new DalNotAPossabilityException("no suche possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : find a specific task \n 4: find all tasks \n 5: update a task\n"); 
            choice = int.Parse(Console.ReadLine());
        }
    }
    public static void DependencyImp()//dependency menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new dependency \n 2 : delete a dependency \n 3 : print a specific dependency \n 4: print all dependencies \n 5: update a dependency\n");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:///create a new dependency
                    Console.Write(s_dal.Dependency.Create(CreateD()));
                    break;
                case 2:///delete a dependency

                    try
                    {
                        int id = Convert.ToInt32(Console.ReadKey());
                        s_dal.Dependency.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:///find a specific dependency
                    int id2 = int.Parse(Console.ReadLine());
                    Console.Write(s_dal.Dependency.Read(id2));
                    break;
                case 4:///find all dependencies
                    Console.Write(s_dal.Dependency.ReadAll());
                    break;
                case 5:///update a dependency
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        Console.Write(s_dal.Dependency.ReadAll());
                        Console.WriteLine("please enter the dependency's updated values");
                        DO.Dependency temp = CreateD();
                        if (temp.Id != 0)
                            s_dal.Dependency.Update(temp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default: throw new Exception("no suche possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new dependency \n 2 : delete a dependency \n 3 : print a specific dependency \n 4: print all dependencies \n 5: update a dependency\n");
            choice = int.Parse(Console.ReadLine());
        }
    }
    public static void EngineerImp()//engineer menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new engineer \n 2 : delete a engineer \n 3 : find a specific engineer \n 4: find all engineers \n 5: update an engineer\n");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch(choice)
            {
                case 1:///create a new engineer
                    try
                    {
                        Console.Write(s_dal.Engineer.Create(CreateE()));
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
                        s_dal.Engineer.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:///find a specific engineer
                    int id2 = Convert.ToInt32(Console.ReadKey());
                    Console.Write(s_dal.Engineer.Read(id2));
                    break;
                case 4:///find all engineers
                    Console.Write(s_dal.Engineer.ReadAll());
                    break;
                case 5:///update an engineers
                    try
                    {
                        Console.Write(s_dal.Engineer.ReadAll());
                        Console.WriteLine("please enter the dependency's updated values");
                        Engineer temp = CreateE();
                        if(temp.Id!=0)
                          s_dal.Engineer.Update(temp);
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
        Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation\n");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1: TaskImp(); break;
                case 2: DependencyImp(); break;
                case 3: EngineerImp(); break;
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
            Initialization.Do(s_dal);
            MainMenu();

        }
		catch (Exception ex)
		{
            Console.Write(ex);
        }
        
			
	}

		
}


