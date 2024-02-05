using BlApi;
using BO;
using DO;
using System.Data.Common;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    
    public void FindDependencies(BO.Task item) //what does it suposed to return?
    {
        //idk how to go over the inumerator

        if (_dal.Task.Read(item.EngineerId) == null)
            return ;
        return ((TaskInEngineer)(from DO.Task task in _dal.Task.ReadAll()
                                 where task.Id == item.EngineerId
                                 select new BO.TaskInEngineer
                                 {
                                     Id = task.Id,
                                     Alias = task.Alias,
                                 }));

        from DO.Task doTask in _dal.Task.ReadAll()
        where (doTask.EngineerId == item.EngineerId)

           /* item.Engineer.Id = doTask.EngineerId;
            item.Engineer.Name = doTask.Engineer.Name;*/




        return;
    }
    public BO.EngineerInTask FindEngineer(BO.Task item)
    {
        return ((BO.EngineerInTask)(from DO.Engineer eng in _dal.Task.ReadAll()
                                 where eng.Id == item.EngineerId
                                 select new BO.EngineerInTask
                                 {
                                     Id = eng.Id,
                                     Name = eng.Name,
                                 }));
    }
    public void AddBeginingDate(BO.Task item, DateTime begin)// are you sure its requiered?
    {
        return;
    }
    public BO.Status FindStatus(BO.Task item)//sets the status of the task
    {
        if (item.StartDate == null)
            return BO.Status.unscheduled;
        if (item.StartDate.Value > DateTime.Now)
            return  BO.Status.scheduled;
        if (item.CompleteDate==null)
            return BO.Status.onTrack;
        if (item.CompleteDate <= DateTime.Now)
            return BO.Status.Done;
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

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        return new BO.Task()
        {
            Id=id,
            Alias = doTask.Alias,
            Description=doTask.Description,
            CreatedAtDate= doTask.CreatedAtDate,
            RequiredEffortTime = doTask.RequiredEffortTime,
            Complexity= (BO.EngineerExperience)doTask.Complexity,
            Deliverables=doTask.Deliverables,
            EngineerId=doTask.EngineerId,
            Remarks =doTask.Remarks,
            ScheduledDate = doTask.ScheduledDate,
            CompleteDate = doTask.CompleteDate,
            DeadLineDate = doTask.DeadLineDate,
            //IsMilestone = doTask.IsMilestone,
            StartDate = doTask.StartDate
            // CurrentYear = (BO.Year)(DateTime.Now.Year - doTask.RegistrationDate.Year)
        };

    }

    /*public IEnumerable<TaskInList> ReadAll()
    {
        throw new NotImplementedException();
    }*/

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {

        if (filter != null)
        {
            return (from DO.Task doTask in _dal.Task.ReadAll()
                    where filter(doTask)
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
                        //IsMilestone = doTask.IsMilestone,
                        StartDate = doTask.StartDate
                        //tYear = (BO.Year)(DateTime.Now.Year - doTask.RegistrationDate.Year)
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
                    //IsMilestone = doTask.IsMilestone,
                    StartDate = doTask.StartDate
                    //tYear = (BO.Year)(DateTime.Now.Year - doTask.RegistrationDate.Year)
                });

    }

    public void Update(BO.Task item)
    {
        DO.Task doTask = new DO.Task(item.Id, item.Alias, item.Description, item.CreatedAtDate, item.RequiredEffortTime, (DO.EngineerExperience)item.Complexity, item.Deliverables,item.Engineer.Id, item.Remarks, item.ScheduledDate, item.CompleteDate, item.DeadLineDate, false, item.StartDate);
        try
        {
            int idTask = _dal.Task.Create(doTask);
            
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }
    }
}
