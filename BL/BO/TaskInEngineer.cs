namespace BO;
/// <summary>
/// represents a task and include its time line
/// </summary>
/// <param name="Id"> identifier of the task</param>
/// <param name="Alias"> a uniqe short name of the task</param>
public class TaskInEngineer
{ 
    public int Id { get; init; }
    public string? Alias { get; set; }
    public override string? ToString() => this.ToStringProperty<TaskInEngineer>();
}
