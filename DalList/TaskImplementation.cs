﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation :ITask 
{
    public int Create(Task item)//create a new task
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return item.Id;
    }

    public void Delete(int id)
    {
        Task taskToDelete = DataSource.Tasks.FirstOrDefault(x => x.Id == id);

        if (taskToDelete != null)
        {
            DataSource.Tasks.RemoveAll(x => x.Id == id);
        }
        else
        {
            throw new DalDeletionImpossible($"Task with ID={id} doesn't exist");
        }
    }

    public Task? Read(int id)
    {
        Task? task = DataSource.Tasks.FirstOrDefault(x => x.Id == id);
        return task;
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;
    }
    public void Update(Task item)
    {
        Task existingTask = DataSource.Tasks.FirstOrDefault(x => x.Id == item.Id);

        if (existingTask != null)
        {
            DataSource.Tasks.Remove(DataSource.Tasks.Find(X => X.Id == item.Id));
            DataSource.Tasks.Add(item);
        }
        else
        {
            throw new DalDoesNotExistException($"The task with Id {item.Id} does not exist");
        }
    }
    public Task? Read(Func<Task, bool> filter)
    {
        var result = DataSource.Tasks; 

        return result.FirstOrDefault(filter); 
    }

    public void DeleteAll()
    {
        DataSource.Tasks.Clear();
    }
}
