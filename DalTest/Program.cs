///mini project roze arenbayev 329335202 and tal biton 329397970

using System.Reflection.Emit;
namespace DalTest;

using DalApi;
using DO;

internal class Program
{
    static readonly IDal s_dal = DalApi.Factory.Get; //stage 4
  
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
        bool flag;
        Console.WriteLine("enter task alias\n");
        string alias = Console.ReadLine();

        Console.WriteLine("enter task description\n");
        string description = Console.ReadLine();

        Console.WriteLine("enter task createdAtDate\n");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime createdAtDate);
        while (flag == false)
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
        Console.ReadKey();
        flag = int.TryParse(Console.ReadLine(), out int hours);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task hours\n");
            flag = int.TryParse(Console.ReadLine(), out hours);
        }
        Console.ReadKey();
        flag = int.TryParse(Console.ReadLine(), out int minuts);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task minuts\n");
            flag = int.TryParse(Console.ReadLine(), out minuts);
        }
        Console.ReadKey();
        flag = int.TryParse(Console.ReadLine(), out int sec);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter task sec\n");
            flag = int.TryParse(Console.ReadLine(), out sec);
        }
        TimeSpan requiredEffortTime = new TimeSpan(days, hours, minuts, sec);

        Console.WriteLine("enter task complexity\n");
        EngineerExperience complexity = SetEX(int.Parse(Console.ReadLine()));

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
        /*Console.WriteLine("enter task alias\n");
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
        int minutes = int.Parse(Console.ReadLine());
        Console.ReadKey();
        int sec = int.Parse(Console.ReadLine());
        TimeSpan requiredEffortTime= new TimeSpan(days,hours,minutes,sec);
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
        DateTime? startDate = DateTime.Parse(Console.ReadLine());*/
        
        DO.Task item = new DO.Task(0, alias, description, createdAtDate, requiredEffortTime, complexity, deliverables, engineerId, remarks, scheduledDate, completeDate, deadLineDate, false, startDate);
        return item;
    }
    public static Dependency CreateD()//creates a new dependency
    {
        bool flag;
        Console.WriteLine("enter dependentTask id\n");
        flag = int.TryParse(Console.ReadLine(), out int dependentTask);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter dependent Task id\n");
            flag = int.TryParse(Console.ReadLine(), out dependentTask);
        }

        Console.WriteLine("enter depend On Task id\n");
        flag = int.TryParse(Console.ReadLine(), out int dependOnTask);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter depend On Task id\n");
            flag = int.TryParse(Console.ReadLine(), out dependOnTask);
        }

        /*Console.WriteLine("enter dependentTask \n ");
        int dependentTask= int.Parse(Console.ReadLine());
        Console.WriteLine("enter dependOnTask \n ");
        int dependOnTask= int.Parse(Console.ReadLine());*/
        DO.Dependency item = new DO.Dependency(0, dependentTask, dependOnTask);
        return item;
    }
    public static Engineer CreateE()//creates a new engineer 
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
        EngineerExperience level = SetEX(Convert.ToInt32(Console.ReadKey()));

        Console.WriteLine("enter engineer cost for hour\n");
        flag = double.TryParse(Console.ReadLine(), out double cost);
        while (flag == false)
        {
            Console.WriteLine("Error, incompetible type was entered \n");
            Console.WriteLine("Enter engineer cost\n");
            flag = double.TryParse(Console.ReadLine(), out cost);
        }
       /* Console.WriteLine("enter engineer id\n");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("enter engineer name\n");
        string name = Console.ReadLine();
        Console.WriteLine("enter engineer email\n");
        string email = Console.ReadLine();
        Console.WriteLine("enter engineer level\n");
        EngineerExperience level = SetEX(int.Parse(Console.ReadLine()));
        Console.WriteLine("enter engineer cost for hour\n");
        double cost = double.Parse(Console.ReadLine());*/
        DO.Engineer item = new DO.Engineer(id, name, email, level, cost);
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
                    Console.Write(s_dal.Task.Create(CreateT()));
                    break;
                case 2:///delete a task
                    try
                    {
                        int.TryParse(Console.ReadLine(), out int id2);
                        s_dal.Task.Delete(id2);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:///find a specific task
                    int.TryParse(Console.ReadLine(), out int id);
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
                default: throw new DalNotAPossabilityException("no such possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : find a specific task \n 4: find all tasks \n 5: update a task\n");
            int.TryParse(Console.ReadLine(), out  choice);
        }
    }
    public static void DependencyImp()//dependency menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new dependency \n 2 : delete a dependency \n 3 : print a specific dependency \n 4: print all dependencies \n 5: update a dependency\n");
        int.TryParse(Console.ReadLine(), out int choice);
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
                        int id = int.Parse(Console.ReadLine());
                        s_dal.Dependency.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:///find a specific dependency
                    int.TryParse(Console.ReadLine(), out int id2);
                    Console.Write(s_dal.Dependency.Read(id2));
                    break;
                case 4:///find all dependencies
                    Console.Write(s_dal.Dependency.ReadAll());
                    break;
                case 5:///update a dependency
                    try
                    {
                        int.TryParse(Console.ReadLine(), out int id);
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
                default: throw new Exception("no such possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new dependency \n 2 : delete a dependency \n 3 : print a specific dependency \n 4: print all dependencies \n 5: update a dependency\n");
            int.TryParse(Console.ReadLine(), out choice);
        }
    }
    public static void EngineerImp()//engineer menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new engineer \n 2 : delete a engineer \n 3 : find a specific engineer \n 4: find all engineers \n 5: update an engineer\n");
        int.TryParse(Console.ReadLine(), out int choice);
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
                        int.TryParse(Console.ReadLine(), out int id);
                        s_dal.Engineer.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:///find a specific engineer
                    Console.WriteLine("please enter engineer's id");
                    int id2 = int.Parse(Console.ReadLine());
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
                default: throw new Exception("no such possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new engineer \n 2 : delete a engineer \n 3 : find a specific engineer \n 4: find all engineers \n 5: update an engineer\n");
            int.TryParse(Console.ReadLine(), out choice);
        }
    }

    static void MainMenu()//menu
    {
        Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation\n 4 : Initialization\n");
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
                case 3:
                    {
                        DependencyImp();  
                        break; 
                    }
                case 4:
                    {
                        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                        if (ans == "Y") //stage 3
                            Initialization.Do(); //stage 2
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
            //Initialization.Do(s_dal);
            MainMenu();

        }
		catch (Exception ex)
		{
            Console.Write(ex);
        }
        
			
	}

		
}


