namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation :IEngineer
{
    public int Create(Engineer item)// creates a new engineer
    {
        // Create a copy of the item with the same Id
        Engineer copy = item with { Id = item.Id };

        // Check if an engineer with the same Id already exists
        if (DataSource.Engineers.Any(e => e.Id == item.Id))
            throw new Exception($"Engineer with ID={item.Id} already exists");

        // Add the copied engineer to the collection of engineers
        DataSource.Engineers.Add(copy);

        // Return the Id of the created engineer
        return item.Id;


    }

    public void Delete(int id) // deletes an engineer
    {
        // Check if an engineer with the given Id exists
        if (DataSource.Engineers.Exists(x => x.Id == id))
        {
            // Remove all engineers with the given Id
            DataSource.Engineers.RemoveAll(x => x.Id == id);
        }
        else
        {
            throw new Exception($"Engineer with ID={id} doesn't exist");
        }
    }

    public Engineer? Read(int id) // reads an engineer
    {
        // Check if an engineer with the given Id exists
        if (DataSource.Engineers.Exists(x => x.Id == id))
        {
            // Find and return the engineer with the given Id
            return DataSource.Engineers.Find(x => x.Id == id);
        }

        // If no engineer with the given Id exists, return null
        return null;
    }

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Engineer
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineer
               select item;
    }

    public void Update(Engineer item)
    {
        var engineerToUpdate = DataSource.Engineers.FirstOrDefault(x => x.Id == item.Id);
        if (engineerToUpdate != null)
        {
            DataSource.Engineers.Remove(engineerToUpdate);
            DataSource.Engineers.Add(item);
        }
        else
        {
            throw new NotImplementedException($"The Engineer with Id {item.Id} does not exist");
        }
    }
}
