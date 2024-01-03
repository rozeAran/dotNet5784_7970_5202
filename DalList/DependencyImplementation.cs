namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
        
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
       // Dependency temp=new Dependency(id,0,0);
        return DataSource.Dependencies.Find(x => x.Id == id);


    }

    public List<Dependency> ReadAll()
    {
        throw new NotImplementedException();
        return new List<T>(DataSource.Ts);
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
