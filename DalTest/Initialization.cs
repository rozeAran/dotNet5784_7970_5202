namespace DalTest;
using DalApi;
using DO;
using System.Data.Common;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;

public static class Initialization
{
    private static IDependency? s_dalDependency; //stage 1
    private static ITask? s_dalTask; //stage 1
    private static IEngineer? s_dalEngineer; //stage 1

    private static readonly Random s_rand = new();
    private static void createDependency()
    {
        int NUMDEP = 40;
        for (int i = 0; i < NUMDEP; i++)
        {
            int dependentTask = s_rand.Next(); ;//number of the task depending on this task
            int dependOnTask = s_rand.Next();//number of the task this task is depending on
                                             
            Dependency newDep = new(0, dependentTask, dependOnTask);
            s_dalDependency!.Create(newDep);
        }
    }
    private static void createTask()
    {
        int NUMTASKS = 25;
        for (int i = 0; i < NUMTASKS; i++) 
        {
            ///Complexity
            int x = s_rand.Next(0, 5);
            EngineerExperience level;
            switch (x)
            {
                case 0:
                    {
                        level =  EngineerExperience.Beginner;
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

            DateTime createdAtDate = DateTime.Now.AddDays(-s_rand.Next());
            TimeSpan requiredEffortTime = TimeSpan.FromDays(s_rand.NextDouble() * s_rand.Next(1, 30));
            int engineerId;
            do
                engineerId = s_rand.Next(100000000, 1000000000);
            while (s_dalEngineer!.Read(engineerId) == null);
            //creates the new engineer 
            Task newTask = new(0, "alias", "Description", createdAtDate, requiredEffortTime,level, "deliverables", engineerId,"remarks", null, null, null,false,null);
            s_dalTask!.Create(newTask);
        }
    

    }
    private static void createEngineer()
    {
        string[] engineerNames =
        {
        "Dani Lnui", "Tamar Yarin", "Yoval Cohen",
        "Ariel Levi", "Dana Klein", "Shira Israelof","Gal Tnzanya",
        "Aharon Yoni"
        };
 

        foreach (var name in engineerNames)
        {
            //engineer's id
            int id;
            do
                id = s_rand.Next(100000000, 1000000000);
            while (s_dalEngineer!.Read(id) != null);

            //engineer's mail
            string email = name+"@jct.com";

            //engineers leval
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
            int MAX = 30;
            int MIN = 200;
            double cost = s_rand.NextDouble() * s_rand.Next(MIN, MAX);

            //creates the new engineer 
            Engineer newEng = new(id, name, email, level, cost);
            s_dalEngineer!.Create(newEng);
        }



    }
}
