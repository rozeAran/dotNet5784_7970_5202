﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        if (Read(item.Id) is not null)
            throw new Exception($"task with ID={item.Id} already exists");
        DataSource.Tasks.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        if (DataSource.Tasks.Exists(X => X.Id == id))
            DataSource.Tasks.RemoveAll(x => x.Id == id);
        else throw new Exception($"task doesnt exsist");
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
