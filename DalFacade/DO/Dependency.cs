
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
    public Dependency() : this(1,1,1) { }//deafult constractor
   

}
