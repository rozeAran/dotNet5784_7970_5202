﻿using BlApi;
using BO;
using DO;
using System.Data.Common;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    
    public List<BO.TaskInList> FindDependencies(DO.Task item) 
    {
        return ((List<BO.TaskInList>)(from DO.Dependency dep in _dal.Dependency.ReadAll()
                                    where dep.DependentTask == item.Id//task is depended on this task
                                    select new BO.TaskInList
                                    {
                                        //a list of all the task this task is depended on
                                        Id = dep.DependOnTask,
                                        Description= _dal.Task.Read(dep.DependOnTask).Description,
                                        Alias= _dal.Task.Read(dep.DependOnTask).Alias,
                                        Status=FindStatus(_dal.Task.Read(dep.DependOnTask))
                                        
                                    }));
    }
    public BO.EngineerInTask FindEngineer(DO.Task item)
    {
        return ((BO.EngineerInTask)(from DO.Engineer eng in _dal.Task.ReadAll()
                                 where eng.Id == item.EngineerId
                                 select new BO.EngineerInTask
                                 {
                                     Id = eng.Id,
                                     Name = eng.Name,
                                 }));
    }
    public void AddBeginingDate(DO.Task item, DateTime begin)
    {
 
    }
    public BO.Status FindStatus(DO.Task item)//sets the status of the task
    {
        if (item.StartDate == null)
            return BO.Status.unscheduled;
        if (item.StartDate.Value > DateTime.Now)
            return  BO.Status.scheduled;
        if (item.CompleteDate==null)
            return BO.Status.onTrack;
        if (item.CompleteDate <= DateTime.Now)
            return BO.Status.Done;
        throw new WrongOrderOfDatesException("impossible order of dates");
    }

    public int Create(BO.Task item)
    {
        DO.Task doTask = new DO.Task(item.Id, item.Alias, item.Description, item.CreatedAtDate, item.RequiredEffortTime, (DO.EngineerExperience)item.Complexity, item.Deliverables,item.Engineer.Id, item.Remarks, item.ScheduledDate, item.CompleteDate, item.DeadLineDate,false, item.StartDate);
        try
        {
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }

    }

    public void Delete(int id)//WE NEED TO CONSIDER THE DEPENDED TASKS
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        _dal.Task.Delete(id);

    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        
        return new BO.Task()
        {
            Id = id,
            Alias = doTask.Alias,
            Description = doTask.Description,
            CreatedAtDate = doTask.CreatedAtDate,
            RequiredEffortTime = doTask.RequiredEffortTime,
            Complexity = (BO.EngineerExperience)doTask.Complexity,
            Deliverables = doTask.Deliverables,
            EngineerId = doTask.EngineerId,
            TaskStatus = FindStatus(doTask),
            Dependencies=FindDependencies(doTask),
            Engineer=FindEngineer(doTask),
            Remarks =doTask.Remarks,
            ScheduledDate = doTask.ScheduledDate,
            CompleteDate = doTask.CompleteDate,
            DeadLineDate = doTask.DeadLineDate,
            StartDate = doTask.StartDate

        };

    }


    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {

        if (filter != null)
        {
            return (from DO.Task doTask in _dal.Task.ReadAll()
                    where filter(Read(doTask.Id))
                    select new BO.Task
                    {
                        Id = doTask.Id,
                        Alias = doTask.Alias,
                        Description = doTask.Description,
                        CreatedAtDate = doTask.CreatedAtDate,
                        RequiredEffortTime = doTask.RequiredEffortTime,
                        Complexity = (BO.EngineerExperience)doTask.Complexity,
                        Deliverables = doTask.Deliverables,
                        EngineerId = doTask.EngineerId,
                        Remarks = doTask.Remarks,
                        ScheduledDate = doTask.ScheduledDate,
                        CompleteDate = doTask.CompleteDate,
                        DeadLineDate = doTask.DeadLineDate,
                        StartDate = doTask.StartDate
                    });
        }
        return (from DO.Task doTask in _dal.Task.ReadAll()
            select new BO.Task
            {
                Id = doTask.Id,
                Alias = doTask.Alias,
                Description = doTask.Description,
                CreatedAtDate = doTask.CreatedAtDate,
                RequiredEffortTime = doTask.RequiredEffortTime,
                Complexity = (BO.EngineerExperience)doTask.Complexity,
                Deliverables = doTask.Deliverables,
                 EngineerId=doTask.EngineerId,
                Remarks = doTask.Remarks,
                ScheduledDate = doTask.ScheduledDate,
                CompleteDate = doTask.CompleteDate,
                DeadLineDate = doTask.DeadLineDate,
                StartDate = doTask.StartDate
                //tYear = (BO.Year)(DateTime.Now.Year - doTask.RegistrationDate.Year)
            });

    }

    public void Update(BO.Task item)
    {
        DO.Task doTask = new DO.Task(item.Id, item.Alias, item.Description, item.CreatedAtDate, item.RequiredEffortTime, (DO.EngineerExperience)item.Complexity, item.Deliverables,item.Engineer.Id, item.Remarks, item.ScheduledDate, item.CompleteDate, item.DeadLineDate, false, item.StartDate);
        try
        {
          _dal.Task.Update(doTask);

        }
        catch (DO.DalAlreadyExistsException ex)
        {
           throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }
    }
}
