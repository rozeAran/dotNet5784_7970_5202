﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
/// <summary>
/// general interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrud<T> where T : class
{
    int Create(T item); //Creates new entity object in DAL
    T? Read(int id); //Reads entity object by its ID 
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null); //stage 1 only, Reads all entity objects
    void Update(T item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
    T? Read(Func<T, bool> filter); // stage 2
    public void DeleteAll();//deleting everything
}


