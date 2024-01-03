namespace DO;
public record Task
{
    int Id { get; set; }
    string Alias { get; set; }
    string Description { get; set; }
    Datetime CreatedAtDate;
    TimeSpan RequiredEffortTime { get; set; }
    bool IsMilestone;
    DO.EngineerExperience Complexity { get; set; }
    Datetime StartDate { get; set; }
    Datetime ScheduledDate { get; set; }
    Datetime DeadLineDate { get; set; }
    Datetime CompleteDate { get; set; }
    string Deliverables { get; set; }
    string Remarks { get; set; }
    int Engineerid { get; set; };

};
