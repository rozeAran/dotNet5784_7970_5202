namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id"> identifier of the object</param>
/// <param name="Alias"> a uniqe short name</param>
/// <param name="Description"> a short description</param>
/// <param name="Id"> identifier of the object</param>
public record Task
{ 
    int Id,
    string Alias;
    string Description;
    Datetime CreatedAtDate;
    TimeSpan RequiredEffortTime;
    bool IsMilestone = false;
    DO.EngineerExperience Complexity;
    Datetime StartDate;
    Datetime ScheduledDate;
    Datetime DeadLineDate;
    Datetime? CompleteDate = null;
    string Deliverables;
    string Remarks;
    int Engineerid;
    enum.Level Level; 


    public Task() : this(0) { }
    public Task(int id,string alias,string description,Datetime createdAtDate,TimeSpan requiredEffortTime,bool isMilestone = false;DO.EngineerExperience complexity,Datetime startDate,Datetime scheduledDate,Datetime deadLineDate,Datetime completeDate, string deliverables,string remarks,int engineerid,enum.Level level) 
     {
        this.Id=id;
        this.Alias=alias;
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
