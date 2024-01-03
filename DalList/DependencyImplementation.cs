namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int rtrn = DataSource.Config.NextDepId;
        Dependency temp=new Dependency(rtrn, item.DependentTask,item.DependOnTask);
        if(Read(rtrn)!=null) { throw new NotImplementedException(); }
        DataSource.Dependencies.Add(temp);
        return rtrn;  
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Dependency> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
