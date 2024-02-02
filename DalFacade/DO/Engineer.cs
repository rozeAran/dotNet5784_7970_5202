namespace DO;

/// <summary>
/// represents an engineer that works on a task and includes usefull information about him/her
/// </summary>
/// <param name="Id"> engineer's id</param>
/// <param name="Name"> engineer's name</param>
/// <param name="Email"> engineer's mail</param>
/// <param name="Level">engineer's experience </param>
/// <param name="Cost">engineer's salary</param>
public record Engineer 
(
    int Id,
    string? Name,
    string? Email,
    EngineerExperience? Level,
    double/*?*/ Cost
)
{
    public Engineer() : this(0,"","", EngineerExperience.Beginner, 0) { }//deafult constractor
   
 }

