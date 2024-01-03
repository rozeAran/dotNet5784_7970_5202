﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int rtrn = DataSource.Config.NextDepId;
        //Dependency temp=new Dependency(rtrn, item.DependentTask,item.DependOnTask);
        Dependency temp = new Dependency with(DataSource.Config.NextDepId,)
        //if(Read(rtrn)!=null) { throw new NotImplementedException(); }
        DataSource.Dependencies.Add(temp);
        return rtrn;  
    }

    public void Delete(int id)
    {
        if (DataSource.Dependencies.Exists(X => X.Id == id))
            DataSource.Dependencies.RemoveAll(x => x.Id == id);
        else throw new Exception($"");


    }

    public Dependency? Read(int id)
    {
        if (DataSource.Dependencies.Exists(X => X.Id == id))
            return DataSource.Dependencies.Find(x => x.Id == id);
        return null;

    }

    public List<Dependency> ReadAll()
    {
        
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        if (DataSource.Dependencies.Exists(X => X.Id == item.Id))
        {
            DataSource.Dependencies.Remove(DataSource.Dependencies.Find(X => X.Id == item.Id));
            DataSource.Dependencies.Add(item);
        }
        else { throw new NotImplementedException($"The task with {item.Id} does not exist"); }
    }
}
