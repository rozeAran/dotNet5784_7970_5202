using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO;
public record BTask
(
    int Id,
    string? Alias,
    string? Description,
    DateTime CreatedAtDate,
    TimeSpan RequiredEffortTime,
    BO.EngineerExperience Complexity,
    string? Deliverables,
    int EngineerId,
    string? Remarks,
    DateTime? ScheduledDate = null,
    DateTime? CompleteDate = null,
    DateTime? DeadLineDate = null,
    bool IsMilestone = false,
    DateTime? StartDate = null,
    datetime ForecastDate,
    BO.Status Status,
    List<BO.TaskInList> Dependencies,
    BO.MilestoneInTask Milestone,
    BO.EngineerInTask Engineer 
)
{

}
