using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalList : IDal
{
    private static readonly DalList instance  = new DalList();

    static DalList() { }
    private DalList() { }
    public static DalList Instance
    {
        get
        {
            return instance;
        }
    }
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public DateTime? StartProjectDate { get { return DataSource.Config.StartProjectDate; } set { DataSource.Config.StartProjectDate = value; } }

    public DateTime? EndProjectDate { get { return DataSource.Config.EndProjectDate; } set { DataSource.Config.EndProjectDate = value; } }

}
