using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO;

public record BEngineer
(
    int Id,
    string Name,
    string Email,
    BO.EngineerExperience Level,
    double Cost,
    BO.TaslInEngineer Task
)
{
    
}
