﻿namespace Dal;
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

    public void Delete(int id)//deletes an engineer
    {
        if (DataSource.Engineers.Exists(X => X.Id == id))
            DataSource.Engineers.RemoveAll(x => x.Id == id);
        else throw new Exception($"Engineer with ID={id} doesnt exsist");

    }

    public Engineer? Read(int id)// reads an engineer
    {
        if (DataSource.Engineers.Exists(X => X.Id == id))
            return DataSource.Engineers.Find(x => x.Id == id);
        return null;
    }

    public List<Engineer> ReadAll()// reads all the engineers
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)// updates an engineer
    {
        if (DataSource.Engineers.Exists(X => X.Id == item.Id))
        {
            DataSource.Engineers.Remove(DataSource.Engineers.Find(X => X.Id == item.Id));
            DataSource.Engineers.Add(item);
        }
        else { throw new NotImplementedException($"The Engineer with Id {item.Id} does not exists"); }
    }
}
