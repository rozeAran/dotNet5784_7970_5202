using BlApi;
using BO;
using DO;
using System.Data.Common;

namespace BlImplementation;
/// <summary>
/// the interface of task in the logical layer 
/// </summary>
/// <method name="FindEngineer"> find the enginner that works on this task</method>
/// <method name="FindDependencies"> findes the tasks that depend on this task</method>
/// <method name="FindStatus">findes the status of the task </method>
/// <method name="Create"> : trys to add a task to the data layer</method>
/// <method name="Read">: returns the task that matches the id </method>
/// <method name="ReadAll">: returns the list of tasks </method>
/// <method name="ReadAll">: returns a list of tasks that matches the function </method>
/// <method name="Update">: if the data is valid, will try to update the task in the data layer </method>
/// <method name="AddBeginingDate">: adding to the task a begining date </method>
/// <method name="Delete">: if the task is deletebul then will delete it </method>
internal class TaskImplementation : ITask
{
    private readonly IBl _bl;
    internal TaskImplementation(IBl bl) => _bl = bl;

    private DalApi.IDal _dal = DalApi.Factory.Get;
    public List<BO.TaskInList> FindDependencies(DO.Task item) //finds the tasks this task is depended on
    {
        return ((List<BO.TaskInList>)(from DO.Dependency dep in _dal.Dependency.ReadAll()
                                    where dep.DependOnTask == item.Id//task is depended on this task
                                    select new BO.TaskInList
                                    {
                                        //a list of all the task this task is depended on
                                        Id = dep.DependentTask,
                                        Description= _dal.Task.Read(dep.DependOnTask).Description,
                                        Alias= _dal.Task.Read(dep.DependOnTask).Alias,
                                        Status=FindStatus(_dal.Task.Read(dep.DependOnTask))
                                        
                                    }));
    }

    private bool hasCircle(int dependentTask, int dependOnTask)
    {
        if (dependentTask == dependOnTask) return true;

        var hasCircleRes = false;
        var dependecies = _dal.Dependency.ReadAll(dependence => dependence.DependentTask == dependOnTask);
        if (dependecies.Any())
        {
            foreach (var dependence in dependecies)
            {
                hasCircleRes = hasCircle(dependentTask, dependence.DependOnTask);
                if (hasCircleRes) return hasCircleRes;
            }
        }

        return hasCircleRes;
    }

    public void AddStartDates()
    {
        if (_dal.StartProjectDate is not null)
        {
            var tasks = _dal.Task.ReadAll().ToDictionary(t => t.Id);
            var dependencies = _dal.Dependency.ReadAll();

            foreach (var task in tasks.Values)
            {
                var dependencies1 = dependencies.Where(d => d.DependentTask == task.Id);

                var dependenciesTasks = dependencies1.Select(t => _dal.Task.Read(t.DependOnTask!)).ToList();

                var startDate = dependenciesTasks switch
                {
                    { Count: 0 } => _dal.StartProjectDate,
                    var d when d.Any(t => t!.StartDate is null) => throw new BO.StartTimeOfDependenceTaskNotExist("There is no start date to the dependent task\n"),
                    _ => getTaskEnd(dependenciesTasks.MaxBy(t => getTaskEnd(t!))!)?.AddDays(new Random().Next(2, 4))
                };

                _dal.Task.Update(task with { StartDate = startDate });
            }
        }
    }
    public BO.EngineerInTask FindEngineer(DO.Task item)//finds the engineer this task is asigned to
    {
        return ((BO.EngineerInTask)(from DO.Engineer eng in _dal.Task.ReadAll()
                                 where eng.Id == item.EngineerId
                                 select new BO.EngineerInTask
                                 {
                                     Id = eng.Id,
                                     Name = eng.Name,
                                 }));
    }
    public void AddBeginingDate(DO.Task item, DateTime begin)//adds a start date
    {
        List<BO.TaskInList> listDep = FindDependencies(item);
        bool theDependencies= listDep.Any<BO.TaskInList>();
        BO.TaskInList taskInList;
        DO.Task task;
        int i= listDep.Count();
        foreach (BO.TaskInList dep in listDep)
        {
            taskInList = listDep.ElementAt(i);
            task =_dal.Task.Read(taskInList.Id);
            if (task.ScheduledDate == null)
                throw new BO.WrongOrderOfDatesException($"task with id:{taskInList.Id} doesnt have scheduled date");
            if(task.ScheduledDate>= begin)
                throw new BO.WrongOrderOfDatesException("order of dates is impossible");
            i--;
        }
       
        DO.Task temp = new DO.Task
        {
            Id = item.Id,
            Alias = item.Alias,
            Description = item.Description,
            CreatedAtDate = item.CreatedAtDate,
            RequiredEffortTime = item.RequiredEffortTime,
            Complexity = item.Complexity,
            Deliverables = item.Deliverables,
            EngineerId = item.Id,
            Remarks = item.Remarks,
            ScheduledDate = begin,
            CompleteDate = item.CompleteDate,
            DeadLineDate = item.DeadLineDate,
            StartDate = item.StartDate
        };
        _dal.Task.Update(temp);
    }
    public BO.Status FindStatus(DO.Task item)//sets the status of the task
    {
        if (item.StartDate == null)
            return BO.Status.Unscheduled;
        if (item.StartDate.Value > _bl.Clock)
            return  BO.Status.Scheduled;
        if (item.CompleteDate==null)
            return BO.Status.OnTrack;
        if (item.CompleteDate <= _bl.Clock)
            return BO.Status.Done;
        throw new WrongOrderOfDatesException("impossible order of dates");
    }

    public int Create(BO.Task item)//creates a new task
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

    public void Delete(int id)//deletes a task
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        _dal.Task.Delete(id);

    }

    public BO.Task? Read(int id)//finds a task
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


    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)//returns a list of tasks that matches the function
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
            });

    }

    public void Update(BO.Task item)// updates a task
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
