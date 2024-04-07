using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
/// <summary>
/// interface for dal- task, engineer and dependency (all readOnly)
/// </summary>
public interface IDal
{
    ITask? Task { get; }
    IEngineer? Engineer { get; }
    IDependency? Dependency { get; }
     DateTime? StartProjectDate { get; set; }
     DateTime? EndProjectDate { get; set; }
}

