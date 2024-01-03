
namespace DO;
/// <summary>
/// represents an engineer that works on a task and includes usefull information about him/her
/// </summary>
public record Engineer
(
    int Id,//engineer's id
    string Name,//engineer's name
    string Email,//engineer's mail
    DO.EngineerExperience Level,//engineer's experience
    double Cost//engineer's salary
)

{
    public Engineer() : this() { }//deafult constractor
    public Engineer(int id, string name, string email, DO.EngineerExperience level, double cost) : this()//all parameter constractor
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.Level = level;
        this.Cost = cost;
    }
 }


