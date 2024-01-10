﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)//creates a dependency
    {
        int id = DataSource.Config.NextDepId;
       // Dependency copy = item with { Id = id };
       //DataSource.Dependencies.Add(copy);
        Dependency copy = new Dependency{  Id = id, DependentTask = item.DependentTask, DependOnTask = item.DependOnTask };
        return item.Id;  
    }

    public void Delete(int id)//deletes a dependency
    {
        if (DataSource.Dependencies.Any(x => x.Id == id))
        {
            DataSource.Dependencies.RemoveAll(x => x.Id == id);//is ok???
        }
        else
        {
            throw new Exception($"Dependency with id {id} doesn't exist");
        }
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;
    }

    public void Update(Dependency item)
    {
        var existingDependency = DataSource.Dependencies.FirstOrDefault(x => x.Id == item.Id);
        if (existingDependency != null)
        {
            DataSource.Dependencies.Remove(existingDependency);
            DataSource.Dependencies.Add(item);
        }
        else
        {
            throw new NotImplementedException($"The Dependency with {item.Id} does not exist");
        }
    }
    Dependency? Read(Func<Dependency, bool> filter)
    {
        var result = DataSource.Dependencies; // קבלת אובייקטים מבסיס הנתונים, יתכן שיהיה צורך להתאים את הקריאה למאגר הנתונים שלך

        return result.Where(filter).ToList(); // החזרת רשימה של כל האובייקטים שמתאימים לסינון המתקבל
    }
}
