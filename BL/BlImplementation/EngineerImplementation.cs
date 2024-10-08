﻿using BlApi;
using BO;
using DO;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlImplementation;
/// <summary>
/// the interface of engineer in the logical layer 
/// </summary>
/// <method name="findTask"> :find the task that the engineer is working on
/// <method name="create"> : trys to add a Engineer to the data layer</method>
/// <method name="read">: returns the Engineer that matches the id </method>
/// <method name="readAll">: returns a list of Engineers that matches the function </method>
/// <method name="ReadAllEngineers">: returns a list of all the engineers </method>
/// <method name="update">: if the data is valid, will try to update the Engineer in the data layer </method>
/// <method name="delete">: if the Engineer is deletebul then will delete it </method>
internal class EngineerImplementation : IEngineer
{
    private readonly IBl _bl;
    internal EngineerImplementation(IBl bl) => _bl = bl;

    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer item)//creates a new engineer
    {

        try
        {
            if (item.Id <= 0)
            {
                throw new BlDataNotValidException("Id is Illegal\n");
            }
            if (item.Name == "")
            {
                throw new BlDataNotValidException("Name is Illegal\n");
            }
            if (item.Cost <= 0)
            {
                throw new BlDataNotValidException("Cost is Illegal\n");
            }

            if (!new EmailAddressAttribute().IsValid(item.Email))
            {
                throw new BlDataNotValidException("Email is Illegal\n");
            }
            DO.Engineer doEngineer = new(item.Id, item.Name, item.Email, (DO.EngineerExperience)item.Level, item.Cost);
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={item.Id} already exists", ex);
        }
        catch(BlDataNotValidException ex) { throw ex; }

    }

    public void Delete(int id)//deletes an engineer
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        BO.Engineer? temp = Read(id);
        if (temp.Task==null) 
        {
            throw new BlCantBeDeletedException($"Engineer with ID={id} cant be deleted");
        }
        _dal.Engineer.Delete(id);
    }

    public BO.Engineer? Read(int id)// finds an engineer
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = FindTask(id)//adding the current task that the engineer is workng on
        };
    }



    public IEnumerable<EngineerInTask> ReadAll()// returns a list of Engineers 
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.EngineerInTask
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                });
    }

    public IEnumerable<BO.Engineer> ReadAllEngineers(Func<BO.Engineer, bool>? filter = null)//read all engineers that match the filter
    {
       if(filter != null)
        {
            return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                    where filter(Read(doEngineer.Id))
                    select new BO.Engineer
                    {
                        Id = doEngineer.Id,
                        Name = doEngineer.Name,
                        Email = doEngineer.Email,
                        Level = (BO.EngineerExperience)doEngineer.Level,
                        Cost = doEngineer.Cost,
                        Task = FindTask(doEngineer.Id)//adding the current task that the engineer is workng on
                    });
        }
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task = FindTask(doEngineer.Id)//adding the current task that the engineer is workng on
                });


    }

    public void Update(BO.Engineer boEngineer)//updates an engineer
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(boEngineer.Id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} does Not exist");

        if ((int)boEngineer.Level < (int)doEngineer.Level)
        {
            throw new BlCantBeUpdetedException($"Engineer with ID={boEngineer.Id} cant be updated");
        }

        if (boEngineer.Id <= 0 )
        {
            throw new BlDataNotValidException("Id is Illegal\n");
        }
        if ( boEngineer.Name == "" )
        {
            throw new BlDataNotValidException("Name is Illegal\n");
        }
        if ( boEngineer.Cost <= 0 )
        {
            throw new BlDataNotValidException("Cost is Illegal\n");
        }

        if ( !new EmailAddressAttribute().IsValid(boEngineer.Email))
        {
            throw new BlDataNotValidException("Email is Illegal\n");
        }
        doEngineer = new(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        _dal.Engineer.Update(doEngineer);
    }

    public BO.TaskInEngineer? FindTask(int id)// finds the task asigned to the engineer
    {
        if (_dal.Task.Read(id) == null) return null;
//            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        return ((TaskInEngineer)(from DO.Task task in _dal.Task.ReadAll()
                where task.Id == id
                select new BO.TaskInEngineer
                {
                    Id = task.Id,
                    Alias = task.Alias,
                }));

    }
}
