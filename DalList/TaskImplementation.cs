namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)//create a new task
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return item.Id;
    }

    public void Delete(int id)//delete a task
    {
        if (DataSource.Tasks.Exists(X => X.Id == id))
            DataSource.Tasks.RemoveAll(x => x.Id == id);
        else throw new Exception($"task with ID={id} doesnt exsist");
    }

    public Task? Read(int id)// find a specific task
    {
        if (DataSource.Tasks.Exists(X => X.Id == id))
            return DataSource.Tasks.Find(x => x.Id == id);
        return null;
    }

    public List<Task> ReadAll()// find all tasks
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)//update a task
    {
        if (DataSource.Tasks.Exists(X => X.Id == item.Id))
        {
            DataSource.Tasks.Remove(DataSource.Tasks.Find(X=>X.Id == item.Id));
            DataSource.Tasks.Add(item);
        }
        else { throw new NotImplementedException($"The task with Id {item.Id} does not exists"); }
    }
}
