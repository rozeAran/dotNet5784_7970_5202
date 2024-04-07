using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
namespace Dal;


//stage 3
sealed internal class DalXml : IDal
{
    private static readonly DalXml instance  = new DalXml();
    private DalXml() { }
    static DalXml() { }
    public static DalXml Instance
    {
        get
        {
            return instance;
        }
    }
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();
    public DateTime? StartProjectDate
    {

        get { return Config.StartProjectDate; }

        set { Config.StartProjectDate = value; }

    }
    public DateTime? EndProjectDate
    {

        get { return Config.EndProjectDate; }

        set { Config.EndProjectDate = value; }
    }
}


