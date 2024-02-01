namespace BO;
public class BTask//maybe we need to make more public
{
    public int Id { get; init; }
    public string? Alias { get; set; }
    public string? Description { get; set; }
    DateTime CreatedAtDate { get; init; }
    TimeSpan RequiredEffortTime { get; set; }
    BO.EngineerExperience Complexity { get; set; }
    public string? Deliverables { get; set; }
    public int EngineerId { get; set; }
    BO.Status Status { get; set; }
    List<BO.TaskInList> Dependencies { get; set; }
    BO.MilestoneInTask Milestone { get; set; }
    BO.EngineerInTask Engineer { get; set; }
    public string? Remarks { get; set; }
    public bool IsMilestone { get; init; }=false;
    DateTime? ForecastDate { get; set; } = null;
    DateTime? ScheduledDate { get; set; } = null;
    DateTime? CompleteDate { get; set; } = null;
    DateTime? DeadLineDate { get; set; } = null;
    DateTime? StartDate { get; init; } = null;
    
}
