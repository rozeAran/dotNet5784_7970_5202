using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public enum EngineerExperience
{
    Beginner,
    AdvancedBeginner,
    Intermediate,
    Advanced,
    Expert
}

public enum Status
{
    unscheduled,
    scheduled,
    onTrack,
   // InJeopardy, relevnt only if we decide to make a secedual look in the general instractions at the end of the definition of logical items
    Done
}


