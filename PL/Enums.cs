﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL; 

public enum Levels
{
    Beginner,
    AdvancedBeginner,
    Intermediate,
    Advanced,
    Expert
}
internal class EngineerExp : IEnumerable
{

    static readonly IEnumerable<BO.EngineerExperience> s_enums =
    (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}



