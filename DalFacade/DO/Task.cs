using System.Data;

namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id"> identifier of the object</param>
/// <param name="Alias"> a uniqe short name</param>
/// <param name="Description"> a short description</param>
/// <param name="CreatedAtDate"> Indicates the time when the task was created by the administrator
/// <param name="RequiredEffortTime"> The amount of time required to perform the task</param>
/// <param name="Complexity"> The difficulty level of the task</param>
/// <param name="ScheduledDate"> iPlanned date for the start of work</param>
/// <param name="CompleteDate"> Actual work completion date - when an engineer reports that he has finished working on the task</param>
/// <param name="Deliverables"> A string describing the results or items provided at the end of the task</param>
/// <param name="EngineerId"> The engineer ID assigned to the task</param>
/// <param name="Remarks"> Remarks about the task</param>
/// <param name="DeadLineDate"> The latest possible date on which the task is finished will not cause the project to fail, so that the entire sequence of tasks that depend on it will be completed before the deadline of the entire project.</param>
/// <param name="IsMilestone"> </param>
/// <param name="StartDate"> Work Start Date - When an engineer begins actual work on the task</param>
public record Task
(
    int Id,
    string Alias,
    string Description,
    DateTime CreatedAtDate,
    TimeSpan RequiredEffortTime,
    EngineerExperience Complexity,
    DateTime ScheduledDate,
    DateTime CompleteDate,
    string Deliverables ,
    int EngineerId,
    string ? Remarks,
    DateTime? DeadLineDate = null,
    bool IsMilestone = false,
    DateTime? StartDate=null
)
{

    public DateTime RegistrationDate => DateTime.Now; //get only
    public Task() : this(1,"","", DateTime.Now, TimeSpan.MinValue, EngineerExperience.Intermediate, DateTime.Now, DateTime.Now, "",1,"",DateTime.Now,false,DateTime.Now) { }
   
}

