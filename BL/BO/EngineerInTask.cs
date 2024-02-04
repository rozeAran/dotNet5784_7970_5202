using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;
/// <summary>
/// represents a task and include its time line
/// </summary>
/// <param name="Id"> identifier of the engineer</param>
/// <param name="Name"> the engineers nane</param>

public class EngineerInTask
{
   public int Id { get; init; }

   public string ?Name { get; set; }

}
