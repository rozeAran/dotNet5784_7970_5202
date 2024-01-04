using Dal;
using DalApi;

namespace DalTest;

internal class Program
{	

	private static ITask? s_dalTask = new TaskImplementation();
	private static IEngineer? s_dalEngineer = new EngineerImplementation();
	private static IDependency? s_dalDependency = new DependencyImplementation();
    public void TaskImp(ITask s_dalTask)
    {

    }
    static void MainMenu()
    {
        Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation");
        int choice = Convert.ToInt32(Console.ReadKey());
        while (choice != 0)
        {
            if (choice == 1)
            {
                s_dalTask
            }
            if (choice == 2)
            {
            }
            if (choice == 3)
            {

            }
            Console.WriteLine("0 : Exit main menu \n 1 : TaskImplementation \n 2 : EngineerImplementation \n 3 : DependencyImplementation");
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





