using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL; 

public enum Levels
{
    Level,
    Beginner,
    AdvancedBeginner,
    Intermediate,
    Advanced,
    Expert,
    None
}
public enum Status
{
    Status,
    None,
    Unscheduled,
    Scheduled,
    OnTrack,
    Done
}

internal class TaskStatus : IEnumerable
{

    static readonly IEnumerable<BO.Status> s_enums =
    (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
internal class EngineerExp : IEnumerable
{

    static readonly IEnumerable<BO.EngineerExperience> s_enums =
    (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}




