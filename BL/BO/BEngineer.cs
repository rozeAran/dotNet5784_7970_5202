using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO;

public class BEngineer
{
    public int Id { get; init; }

    public string Name { get; set; }

    public string Email { get; set; }

    BO.EngineerExperience Level { get; set; }

    public double Cost { get; set; }

    BO.TaskInEngineer Task { get; set; }

}
