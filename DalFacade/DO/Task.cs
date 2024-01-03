using System.Data;

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
    public Task() : this(1,"","",DataSetDateTime.Local, TimeSpan.FromSeconds, EngineerExperience.Intermediate, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, "",1,"",false, DateTime.UtcNow) { }
   
}

