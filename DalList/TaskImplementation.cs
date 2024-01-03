namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        if (DataSource.Tasks.Exists(X => X.Id == id))
            return DataSource.Tasks.Find(x => x.Id == id);
        return null;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        if (DataSource.Tasks.Exists(X => X.Id == item.Id))
        {
            DataSource.Tasks.Remove(DataSource.Tasks.Find(X=>X.Id == item.Id));
            DataSource.Tasks.Add(item);
        }
        else { throw new NotImplementedException("The task with this id does not exists"); }
    }
}
