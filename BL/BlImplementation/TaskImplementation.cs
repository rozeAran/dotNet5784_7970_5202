using BlApi;
using BO;
using DO;
using System.Data.Common;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public DateTime? ForecastDate()//what does it suposed to return?
    {
        DateTime? dateTime = new DateTime();
        return dateTime;
    }
    public List<BO.TaskInList> dependencies() //what does it suposed to return?
    {
        List<BO.TaskInList> taskInLists = new List<BO.TaskInList>();
        return taskInLists;
    }
    public BO.EngineerInTask engineer(DO.Engineer e)
    {
        BO.EngineerInTask engineerInTask=new BO.EngineerInTask(e.Id,e.Name);//we need a constractor but they told us no to make one
        return engineerInTask;
    }
    public BO.Status status()//how to calculate the status?
    {
        BO.Status status = new BO.Status();
        return status;
    }
    public void AddBeginingDate(BO.Task item, DateTime begin)
    {
       
    }

    public int Create(BO.Task item)
    {
        DO.Task doTask = new DO.Task(item.Id, item.Alias, item.Description, item.CreatedAtDate, item.RequiredEffortTime, (DO.EngineerExperience)item.Complexity, item.Deliverables, item.EngineerId, item.Remarks, item.ScheduledDate, item.CompleteDate, item.DeadLineDate,false, item.StartDate);
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
            EngineerId = doTask.EngineerId,
            Remarks=doTask.Remarks,
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

    public void Update(BO.Task item)
    {
        DO.Task doTask = new DO.Task(item.Id, item.Alias, item.Description, item.CreatedAtDate, item.RequiredEffortTime, (DO.EngineerExperience)item.Complexity, item.Deliverables, item.EngineerId, item.Remarks, item.ScheduledDate, item.CompleteDate, item.DeadLineDate, false, item.StartDate);
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
