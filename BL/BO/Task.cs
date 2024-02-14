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
/// <param name="Remarks"> Remarks about the task(notes)</param>
/// <param name="DeadLineDate"> The latest possible date on which the task is finished will not cause the project to fail, so that the entire sequence of tasks that depend on it will be completed before the deadline of the entire project.</param>
/// <param name="StartDate"> Work Start Date - When an engineer begins actual work on the task</param>
/// <param name="TaskStatus"> status of task</param>
/// <param name="Dependencies"> the tasks this task is depende on</param>
/// <param name="Engineer"> engineer this task asigned to</param>
public class Task
{
    public int Id { get; init; }
    public string? Alias { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAtDate { get; init; }
    public TimeSpan RequiredEffortTime { get; set; }
    public BO.EngineerExperience Complexity { get; set; }
    public string? Deliverables { get; set; }
    public int EngineerId { get; set; }
    public BO.Status TaskStatus { get; set; }
    public List<BO.TaskInList>? Dependencies { get; set; }
    public BO.EngineerInTask? Engineer { get; set; }
    public string? Remarks { get; set; }
    //public bool IsMilestone { get; init; }=false;
    public DateTime? ScheduledDate { get; set; } = null;
    public DateTime? CompleteDate { get; set; } = null;
    public DateTime? DeadLineDate { get; set; } = null;
    public DateTime? StartDate { get; init; } = null;

    public override string? ToString() => this.ToStringProperty<Task>();

}
