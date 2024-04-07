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
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
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


