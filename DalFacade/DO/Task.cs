namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id"> identifier of the object</param>
/// <param name="Alias"> a uniqe short name</param>
/// <param name="Description"> a short description</param>
/// <param name="Id"> identifier of the object</param>
public record Task
(
    int Id,
    string Alias,
    string Description,
    DateTime CreatedAtDate,
    TimeSpan RequiredEffortTime,
    bool IsMilestone = false,
    DO.EngineerExperience ?Complexity=null,
    DateTime? StartDate = null,
    DateTime? ScheduledDate = null,
    DateTime? DeadLineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? Engineerid = null,
    Level Level =
)
{

    public DateTime RegistrationDate => DateTime.Now; //get only
    public Task() : this(0) { }
    public Task(int id, string alias, string description, DateTime createdAtDate, TimeSpan requiredEffortTime, bool isMilestone ; DO.EngineerExperience complexity, DateTime startDate=null,DateTime scheduledDate, DateTime deadLineDate,DateTime completeDate, string deliverables,string remarks,int engineerid,Level level)) :this()
    {
        this.Id= id;
        this.Alias= alias;
        this.Description=description;
        this.CreatedAtDate=createdAtDate;
        this.RequiredEffortTime=requiredEffortTime;
        this.IsMilestone=isMilestone;
        this.Complexity=complexity;
        this.StartDate=startDate;
        this.ScheduledDate=scheduledDate;
        this.DeadLineDate=deadLineDate;
        this.CompleteDate=completeDate;
        this.Deliverables=deliverables;
        this.Remarks=remarks;
        this.Engineerid=engineerid;
        this.Level=level;
    }
}

