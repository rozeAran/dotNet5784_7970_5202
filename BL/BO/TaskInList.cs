using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// represents a task and include its time line
/// </summary>
/// <param name="Id"> identifier of the task</param>
/// <param name="Alias"> a uniqe short name of the task</param>
/// <param name="Description"> a short description of the task</param>
/// <param name="Status"> the status of the task

public class TaskInList
{ 
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public BO.Status Status { get; set; }
}
