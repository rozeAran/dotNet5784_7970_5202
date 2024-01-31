using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalApi;
namespace Dal;


//stage 3
sealed internal class DalXml : IDal
{
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();
    public static IDal Instance { get; } => new DalXml();//stage 4
    private DalXml() { }//stage 4
}


