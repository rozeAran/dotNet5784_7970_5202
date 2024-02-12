﻿namespace BlApi;
/// <summary>
/// the interface of task in the logical layer 
/// </summary>
/// <method name="FindDependencies">finds the task this task is depended on </method>
/// <method name="FindEngineer"> finds the engineer this task is asigned to</method>
/// <method name="FindStatus"> sets the status of the task</method>
/// <method name="Create"> : trys to add a task to the data layer</method>
/// <method name="Read">: returns the task that matches the id </method>
/// <method name="ReadAll">: returns a list of tasks that matches the function </method>
/// <method name="Update">: if the data is valid, will try to update the task in the data layer </method>
/// <method name="AddBeginingDate">: adding to the task a begining date </method>
/// <method name="Delete">: if the task is deletebul then will delete it </method>

public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null);
    public void Update(BO.Task item);
    public void Delete(int id);
    public void AddBeginingDate(DO.Task item, DateTime begin);
    public List<BO.TaskInList> FindDependencies(DO.Task item);
    public BO.EngineerInTask FindEngineer(DO.Task item);
    public BO.Status FindStatus(DO.Task item);
}
    
      
