using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO;
/// <summary>
/// represents an engineer that works on a task and includes usefull information about him/her
/// </summary>
/// <param name="Id"> engineer's id</param>
/// <param name="Name"> engineer's name</param>
/// <param name="Email"> engineer's mail</param>
/// <param name="Level">engineer's experience </param>
/// <param name="Cost">engineer's salary</param>
/// <param name="Tasks"> the tasks asigned to the engineer</param>
public class Engineer
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public string? Email { get; set; }

    public BO.EngineerExperience Level { get; set; } = EngineerExperience.Beginner;

    public double? Cost { get; set; }

    public BO.TaskInEngineer? Task { get; set; }

    public override string? ToString() => this.ToStringProperty<Engineer>();


}
