
namespace DO;
/// <summary>
/// represents the dependency between tasks
/// </summary>
/// <param name="Id"> dependency id</param>
/// <param name="DependentTask">ID number of depending task </param>
/// <param name="DependOnTask">Previous task ID number,DependOnTask needs to be finished before the DependentTask </param>
public record Dependency
(
    int Id,
    int DependentTask,
    int DependOnTask
)
{
    public Dependency() : this(0, 0, 0) { }//deafult constractor
}