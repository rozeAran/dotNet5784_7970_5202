namespace DalTest;
using DalApi;
using DO;
using System.Data.Common;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Dal;
using System.Xml.Linq;
using System.Threading.Tasks;

public static class Initialization
{
    private static IDal? s_dal;//stage 2

    private static readonly Random s_rand = new();
    private static void CreateEngineer()// initialization of an engineer
    {
        string[] engineerNames =
        {
        "Dani Lnui", "Tamar Yarin", "Yoval Cohen",
        "Ariel Levi", "Dana Klein", "Shira Israelof","Gal Tnzanya",
        "Aharon Yoni","Yahel Koper","Oded Paz","Simon Yalon","Saked Yair","Yehoda Davidi"
        };


        foreach (var name in engineerNames)
        {
            //engineer's id
            int id;
            do
                id = s_rand.Next(200000000,400000000);
            while (s_dal!.Engineer.Read(id) != null);

            //engineer's mail
            string email = name + "@jct.com";

            //engineers level
            int x = s_rand.Next(0, 5);
            EngineerExperience level;
            switch (x)
            {
                case 0:
                    {
                        level = EngineerExperience.Beginner;
                        break;
                    }
                case 1:
                    {
                        level = EngineerExperience.AdvancedBeginner;//engineer's experience
                        break;
                    }
                case 2:
                    {
                        level = EngineerExperience.Intermediate;//engineer's experience
                        break;
                    }
                case 3:
                    {
                        level = EngineerExperience.Advanced;//engineer's experience
                        break;
                    }
                case 4:
                    {
                        level = EngineerExperience.Expert;//engineer's experience
                        break;
                    }
                default:
                    level = EngineerExperience.Beginner;
                    break;
            }

            //engineer's salary
            int MAX = 500;
            int MIN = 30;
            double cost = s_rand.NextDouble() * s_rand.Next(MIN, MAX);

            //creates the new engineer 
            Engineer newEng = new(id, name, email, level, cost);
            s_dal!.Engineer.Create(newEng);
        }



    }
    private static void CreateTask()// initialization of a task
    {
        string[] description =
        {
        "Recruitment",
        "Division of teams",
        "obtaining resources",
        "budget Division",
        "Risk Analysis",
        "level 1 planning ",
        "level 1 Execution writing the code",
        "level 1 Execution debug",
        "level 1 documentation ",
        "Final tests in level 1",
        "level 2 planning",
        "level 2 Execution writing the code",
        "level 1 Execution debug",
        "level 2 documentation ",
        "Final tests in level 2",
        "level 3 planning",
        "level 3 Execution writing the code",
        "level 1 Execution debug",
        "level 3 documentation ",
        "Final tests in level 3",
        };
        int NUMTASKS = 20;
        for (int i = 0; i < NUMTASKS; i++)
        {
            ///Complexity
            int x = s_rand.Next(0, 5);
            EngineerExperience level;
            switch (x)
            {
                case 0:
                    {
                        level = EngineerExperience.Beginner;
                        break;
                    }
                case 1:
                    {
                        level = EngineerExperience.AdvancedBeginner;//engineer's experience
                        break;
                    }
                case 2:
                    {
                        level = EngineerExperience.Intermediate;//engineer's experience
                        break;
                    }
                case 3:
                    {
                        level = EngineerExperience.Advanced;//engineer's experience
                        break;
                    }
                case 4:
                    {
                        level = EngineerExperience.Expert;//engineer's experience
                        break;
                    }
                default:
                    level = EngineerExperience.Beginner;
                    break;
            }

            DateTime createdAtDate = DateTime.Now.AddDays(-s_rand.Next(5,30));
            DateTime deadLineDate=DateTime.Now.AddDays(s_rand.Next(10,30));
            TimeSpan requiredEffortTime = deadLineDate - createdAtDate;
            
            int engineerId=0;
            //do
                //engineerId = s_rand.Next(200000000, 400000000);
           // while (s_dal!.Engineer.Read(engineerId) == null);
            //creates the new engineer 
            DO.Task newTask = new(0, $"task{i}", description[i], createdAtDate, requiredEffortTime, level, "deliverables", engineerId, "remarks", null, null, deadLineDate, false, null);
            s_dal!.Task.Create(newTask);
        }


    }
    private static void CreateDependency()// initialization of a dependency
    {
        int NUMDEP = 40;
        for (int i = 0; i < NUMDEP; i++)
        {
            int dependentTask;///ID number of depending task
            do
                dependentTask =s_rand.Next(0, 20);
            while ((s_dal!.Task.Read(dependentTask) == null) );
            int dependOnTask;///Previous task ID number
            do
            {
                dependOnTask = s_rand.Next(0,50);
                if ((s_dal!.Task.Read(dependOnTask) != null))
                    if ((s_dal!.Task.Read(dependOnTask).DeadLineDate <= s_dal!.Task.Read(dependentTask).ScheduledDate))///if the time line fits
                        break;
            }
            while ((s_dal!.Task.Read(dependOnTask) == null));
            Dependency newDep = new(0, dependentTask, dependOnTask);
            s_dal!.Dependency.Create(newDep);

        }
    }
   
  

    public static void Do(IDal dal)// initialization
    {
        
        //deleting everything before the initialization
       
        foreach (var task in s_dal!.Task.ReadAll())
        {
            s_dal!.Task.Delete(task.Id); ;
        }
        foreach (var dep in s_dal.Dependency.ReadAll())
        {
            s_dal!.Dependency.Delete(dep.Id); ;
        }
        foreach(var eng in s_dal.Engineer.ReadAll())
        {
            s_dal!.Dependency.Delete(eng.Id); ;
        }
        //s_dal.Dependency.Delete;

        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        CreateEngineer();
        CreateTask();
        CreateDependency();        
    }
}
