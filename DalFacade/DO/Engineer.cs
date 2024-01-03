
namespace DO;

/// <summary>
/// represents an engineer that works on a task and includes usefull information about him/her
/// </summary>
public record Engineer
(
    int Id,//engineer's id
    string Name,//engineer's name
    string Email,//engineer's mail
    EngineerExperience Level,//engineer's experience
    double Cost//engineer's salary
)

{
    public Engineer() : this(1,"","", EngineerExperience.Beginner, 1) { }//deafult constractor
   
 }

