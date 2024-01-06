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

    public static EngineerExperience setEX(int num)
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

    public static DO.Task createT()
    {
        int id = Convert.ToInt32(Console.ReadKey());
        string alias = Console.ReadLine();
        string description = Console.ReadLine();
        DateTime createdAtDate = Convert.ToDateTime(Console.Read());
        TimeSpan requiredEffortTime= Convert.ToTimeSpan(Console.Read()); ;
        int temp= Convert.ToInt32(Console.ReadKey());
        EngineerExperience complexity=setEX(Convert.ToInt32(Console.ReadKey()));
        string deliverables = Console.ReadLine();
        int engineerId = Console.Read();
        string? remarks = Console.ReadLine();
        DateTime? scheduledDate = Convert.ToDateTime(Console.Read());
        DateTime? completeDate = Convert.ToDateTime(Console.Read());
        DateTime? deadLineDate = Convert.ToDateTime(Console.Read());
        bool isMilestone = Convert.ToBoolean(Console.Read());
        DateTime? startDate = Convert.ToDateTime(Console.Read());
        DO.Task item = new DO.Task(id, alias, description, createdAtDate, requiredEffortTime, complexity, deliverables, engineerId, remarks, scheduledDate, completeDate, deadLineDate, isMilestone, startDate);
        return item;
    }
    public static DO.Dependency createD()
    {
        int id= Convert.ToInt32(Console.ReadKey());
        int dependentTask= Convert.ToInt32(Console.ReadKey());
        int dependOnTask= Convert.ToInt32(Console.ReadKey());
        DO.Dependency item = new DO.Dependency(id, dependentTask, dependOnTask);
        return item;
    }
    public static DO.Engineer createE() 
    {
        int id = Convert.ToInt32(Console.ReadKey());
        string name = Console.ReadLine();
        string email = Console.ReadLine();
        EngineerExperience level = setEX(Convert.ToInt32(Console.ReadKey()));
        double cost = Convert.ToDouble(Console.ReadKey());
        DO.Engineer item = new DO.Engineer(id, name, email, level, cost);
        return item;
    }

    public static void TaskImp()
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new task \n 2 : delete a task \n 3 : find a specific task \n 4: find all tasks \n 5: update a task\n");
        int choice = Convert.ToInt32(Console.ReadKey());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    Console.Write(s_dalTask.Create(createT()));
                    break;
                case 2:
                    try
                    {
                        int id2 = Convert.ToInt32(Console.ReadKey());
                        s_dalTask.Delete(id2);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:
                    int id = Convert.ToInt32(Console.ReadKey());
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
                        DO.Task temp = createT();
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
            choice = Convert.ToInt32(Console.ReadKey());
        }
    }
    public static void DependencyImp()
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new dependency \n 2 : delete a dependency \n 3 : find a specific dependency \n 4: find all dependencies \n 5: update a dependency\n");
        int choice = Convert.ToInt32(Console.ReadKey());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    Console.Write(s_dalDependency.Create(createD()));
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
                    int id2 = Convert.ToInt32(Console.ReadKey());
                    Console.Write(s_dalDependency.Read(id2));
                    break;
                case 4:
                    Console.Write(s_dalDependency.ReadAll());
                    break;
                case 5:
                    try
                    {
                        int id = Convert.ToInt32(Console.ReadKey());
                        Console.Write(s_dalDependency.ReadAll());
                        Console.WriteLine("please enter the dependency's updated values");
                        DO.Dependency temp = createD();
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
            choice = Convert.ToInt32(Console.ReadKey());
        }
    }
    public static void EngineerImp()
    {
        Console.WriteLine("0 : Exit task menu \n 1 : create a new engineer \n 2 : delete a engineer \n 3 : find a specific engineer \n 4: find all engineers \n 5: update an engineer\n");
        int choice = Convert.ToInt32(Console.ReadKey());
        while (choice != 0)
        {
            switch(choice)
            {
                case 1:
                    try
                    {
                        Console.Write(s_dalEngineer.Create(createE()));
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
                        Engineer temp = createE();
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

    static void MainMenu()
    {
        Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation\n");
        int choice = Convert.ToInt32(Console.ReadKey());
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
            choice = Convert.ToInt32(Console.ReadKey());
        }

    }
   
    static void Main(string[] args)
	{
		try
		{
            Initialization.Do(s_dalDependency, s_dalTask, s_dalEngineer);
            MainMenu();

        }
		catch (Exception ex)
		{
            Console.Write(ex);
        }
			
	}

		
}


