
namespace DO;
/// <summary>
/// represents the dependency between tasks
/// </summary>
public record Dependency
(
    int Id,//task number
    int DependentTask,//number of the task depending on this task
    int DependOnTask//number of the task this task is depending on
)

{
    public Dependency() : this() { }//deafult constractor
    public Dependency(int id, int dependentTask, int dependOnTask) :this() //all parameter constractor
{
        this.Id = id;
        this.DependentTask = dependentTask;
        this.DependOnTask = dependOnTask;
    }

}
