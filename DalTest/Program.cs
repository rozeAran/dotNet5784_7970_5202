//mini project ex1 roz arenbaiyev 329335202 and tal biton 329397970

using System.Reflection.Emit;
using Dal;
using DalApi;
using DO;


namespace DalTest;

internal class Program
{

    private static ITask? s_dalTask = new TaskImplementation();
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static IDependency? s_dalDependency = new DependencyImplementation();

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

    public static DO.Task CreateT()//creates a new task
    {
        Console.WriteLine("enter id,alias,description of the task,creation date,the required Effort Time," +
            "the tasks complexity, deliverables, engineerId, remarks, scheduledDate,completeDate,deadLineDate" +
            "is the task is a mile stone, start date");
        int id = int.Parse(Console.ReadLine());
        string alias = Console.ReadLine();
        string description = Console.ReadLine();
        DateTime createdAtDate = DateTime.Parse(Console.ReadLine());
        int days = int.Parse(Console.ReadLine());
        Console.ReadKey();
        int hours= int.Parse(Console.ReadLine());
        Console.ReadKey();
        int sec = int.Parse(Console.ReadLine());
        Console.ReadKey();
        int minutes = int.Parse(Console.ReadLine());
        TimeSpan requiredEffortTime= new TimeSpan(days,hours,sec,minutes); 
        int temp= int.Parse(Console.ReadLine());
        EngineerExperience complexity=SetEX(int.Parse(Console.ReadLine()));
        string deliverables = Console.ReadLine();
        int engineerId = Console.Read();
        string? remarks = Console.ReadLine();
        DateTime? scheduledDate = DateTime.Parse(Console.ReadLine());
        DateTime? completeDate = DateTime.Parse(Console.ReadLine());
        DateTime? deadLineDate = DateTime.Parse(Console.ReadLine());
        bool isMilestone = bool.Parse(Console.ReadLine());
        DateTime? startDate = DateTime.Parse(Console.ReadLine());
        DO.Task item = new DO.Task(id, alias, description, createdAtDate, requiredEffortTime, complexity, deliverables, engineerId, remarks, scheduledDate, completeDate, deadLineDate, isMilestone, startDate);
        return item;
    }
    public static DO.Dependency CreateD()//creates a new dependency
    {
        int id= int.Parse(Console.ReadLine());
        int dependentTask= int.Parse(Console.ReadLine());
        int dependOnTask= int.Parse(Console.ReadLine());
        DO.Dependency item = new DO.Dependency(id, dependentTask, dependOnTask);
        return item;
    }
    public static DO.Engineer CreateE()//creates a new engineer 
    {
        int id = int.Parse(Console.ReadLine());
        string name = Console.ReadLine();
        string email = Console.ReadLine();
        EngineerExperience level = SetEX(Convert.ToInt32(Console.ReadKey()));
        double cost = double.Parse(Console.ReadLine());
        DO.Engineer item = new DO.Engineer(id, name, email, level, cost);
        return item;
    }

    public static void TaskImp()//task menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : find a specific task \n 4: find all tasks \n 5: update a task\n");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    Console.Write(s_dalTask.Create(CreateT()));
                    break;
                case 2:
                    try
                    {
                        int id2 = int.Parse(Console.ReadLine());
                        s_dalTask.Delete(id2);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:
                    int id = int.Parse(Console.ReadLine());
                    Console.Write(s_dalTask.Read(id));
                    break;
                case 4:
                    Console.Write(s_dalTask.ReadAll());
                    break;
                case 5:
                    try
                    {
                        Console.Write(s_dalTask.ReadAll());
                        Console.WriteLine("please enter the  task's updated values");
                        DO.Task temp = CreateT();
                        if(temp.ScheduledDate != null)
                          s_dalTask.Update(temp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default: throw new Exception("no suche possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : find a specific task \n 4: find all tasks \n 5: update a task\n"); 
            choice = int.Parse(Console.ReadLine());
        }
    }
    public static void DependencyImp()//dependency menu
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new dependency \n 2 : delete a dependency \n 3 : find a specific dependency \n 4: find all dependencies \n 5: update a dependency\n");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    Console.Write(s_dalDependency.Create(CreateD()));
                    break;
                case 2:

                    try
                    {
                        int id = Convert.ToInt32(Console.ReadKey());
                        s_dalDependency.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:
                    int id2 = int.Parse(Console.ReadLine());
                    Console.Write(s_dalDependency.Read(id2));
                    break;
                case 4:
                    Console.Write(s_dalDependency.ReadAll());
                    break;
                case 5:
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        Console.Write(s_dalDependency.ReadAll());
                        Console.WriteLine("please enter the dependency's updated values");
                        DO.Dependency temp = CreateD();
                        if (temp.Id != 0)
                            s_dalDependency.Update(temp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default: throw new Exception("no suche possibility");
            }
            Console.WriteLine("0 : Exit task menu \n 1 : create a new dependency \n 2 : delete a dependency \n 3 : find a specific dependency \n 4: find all dependencies \n 5: update a dependency\n");
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
                case 1:
                    try
                    {
                        Console.Write(s_dalEngineer.Create(CreateE()));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 2:

                    try
                    {
                        int id = Convert.ToInt32(Console.ReadKey());
                        s_dalEngineer.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:
                    int id2 = Convert.ToInt32(Console.ReadKey());
                    Console.Write(s_dalEngineer.Read(id2));
                    break;
                case 4:
                    Console.Write(s_dalEngineer.ReadAll());
                    break;
                case 5:
                    try
                    {
                        Console.Write(s_dalEngineer.ReadAll());
                        Console.WriteLine("please enter the dependency's updated values");
                        Engineer temp = CreateE();
                        if(temp.Id!=0)
                          s_dalEngineer.Update(temp);
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
                default: throw new Exception("no suche possibility");
            }
            Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation\n");
            choice = int.Parse(Console.ReadLine());
        }

    }
   
    static void Main(string[] args)
	{
		try
		{
            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
            MainMenu();

        }
		catch (Exception ex)
		{
            Console.Write(ex);
        }
        
			
	}

		
}


