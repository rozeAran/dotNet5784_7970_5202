namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        if (DataSource.Engineers.Exists(X => X.Id == id))
            return DataSource.Engineers.Find(x => x.Id == id);
        return null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        if (DataSource.Engineers.Exists(X => X.Id == item.Id))
        {
            DataSource.Engineers.Remove(DataSource.Engineers.Find(X => X.Id == item.Id));
            DataSource.Engineers.Add(item);
        }
        else { throw new NotImplementedException("The task with this id does not exists"); }
    }
}
