using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO;
/// <summary>
/// represents a task and include its time line
/// </summary>
/// <param name="Id"> identifier of the object</param>
/// <param name="Alias"> a uniqe short name</param>
/// <param name="Description"> a short description</param>
/// <param name="CreatedAtDate"> Indicates the time when the task was created by the administrator
/// <param name="RequiredEffortTime"> The amount of time required to perform the task</param>
/// <param name="Complexity"> The difficulty level of the task</param>
/// <param name="ScheduledDate"> Planned date for the start of work</param>
/// <param name="CompleteDate"> Actual work completion date - when an engineer reports that he has finished working on the task</param>
/// <param name="Deliverables"> A string describing the results or items provided at the end of the task</param>
/// <param name="EngineerId"> The engineer ID assigned to the task</param>
/// <param name="Remarks"> Remarks about the task</param>
/// <param name="DeadLineDate"> The latest possible date on which the task is finished will not cause the project to fail, so that the entire sequence of tasks that depend on it will be completed before the deadline of the entire project.</param>
/// <param name="IsMilestone"> </param>
/// <param name="StartDate"> Work Start Date - When an engineer begins actual work on the task</param>
public class Engineer
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public string? Email { get; set; }

    public BO.EngineerExperience Level { get; set; } = EngineerExperience.Beginner;

    public double ?Cost { get; set; }

    public Tuple<int,string> ?Task { get; set; }

    //public override string? ToString() => this.ToStringProperty();


}
