using BlApi;
using BO;
using DO;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
/// <method name="AddbeginingDate">: adding to the task a begining date </method>
/// <method name="Delete">: if the task is deletebul then will delete it </method>
internal class TaskImplementation : ITask
{
    private readonly IBl _bl;
    internal TaskImplementation(IBl bl) => _bl = bl;

    private DalApi.IDal _dal = DalApi.Factory.Get;
    public List<BO.TaskInList>? FindDependencies(DO.Task item) //finds the tasks this task is depended on
    {
        var temp= (from DO.Dependency dep in _dal.Dependency.ReadAll()
                   where dep.DependentTask == item.Id//task is depended on this task
                   select new BO.TaskInList
                   {
                       //a list of all the task this task is depended on
                       Id = dep.DependOnTask,
                       Description = _dal.Task.Read(dep.DependentTask).Description,
                       Alias = _dal.Task.Read(dep.DependentTask).Alias,
                       Status = FindStatus(_dal.Task.Read(dep.DependentTask))

                   });
        
        List<BO.TaskInList> lst=temp.ToList();
        return lst;
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

    public void AddScheduledDates()
    {
        if (_dal.StartProjectDate is not null)
        {
            var tasks = _dal.Task.ReadAll().ToDictionary(t => t.Id);
            var dependencies = _dal.Dependency.ReadAll();

            foreach (var task in tasks.Values)
            {
                var dependencies1 = dependencies.Where(d => d.DependentTask == task.Id);//מה שתלוי בטסק

                var dependenciesTasks = dependencies1.Select(t => _dal.Task.Read(t.DependOnTask!)).ToList();

                var scheduledDate = dependenciesTasks switch
                {
                    { Count: 0 } => _dal.StartProjectDate,
                    var d when d.Any(t => t!.ScheduledDate is null) => throw new BO.StartTimeOfDependenceTaskNotExist("There is no start date to the dependent task\n"),
                    _ => getTaskEnd(dependenciesTasks.MaxBy(t => getTaskEnd(t!))!)?.AddDays(new Random().Next(2, 4))
                };

                _dal.Task.Update(task with { ScheduledDate = scheduledDate });
            }
        }
    }

    public bool IsTaskCanBeAssigntToWorker(BO.Engineer engineer, BO.Task task)
    {
        //ask for all the tasks that 'this task'  depends on them, and then take only the numbers of these tasks
        var dependensTasksIds = _dal.Dependency.ReadAll(task => task.DependentTask == task.Id).Select(t => t!.DependOnTask)
        .ToHashSet();//in the hashset i have all the numbers of dependend tasks

        //we check if all the tasks that 'task' dependents on them are done
        var dependensTasks = _dal.Task.ReadAll(task => dependensTasksIds.Contains(task.Id) && task!.CompleteDate is null &&
        FindStatus(task) == BO.Status.Done);
        // var dependensTasks = _dal.Task.ReadAll(task => dependensTasksIds.Contains(task.Id));

        // return worker?.Role==task.Complexity&&!dependensTasks.Any();
        return task.Engineer is null && dependensTasks.All(task => task.CompleteDate is null) &&
            engineer?.Level == task.Complexity;
    }

    private DateTime? getTaskEnd(DO.Task item)
    {
        return item.CompleteDate; //fix
    }

    public BO.EngineerInTask FindEngineer(DO.Task item)//finds the engineer this task is asigned to
    {

        if (item.EngineerId == 0)
            return null;
       var temp= (from DO.Engineer eng in _dal.Task.ReadAll()
                  where eng.Id == item.EngineerId
                  select new BO.EngineerInTask
                  {
                      Id = eng.Id,
                      Name = eng.Name,
                  });
       BO.EngineerInTask tempE= temp.First();
        return tempE;
    }

    public void AddBeginingDateBO(BO.Task item, DateTime? begin)
    {
        AddBeginingDate(_dal.Task.Read(item.Id), begin);
    }

    public void AddBeginingDate(DO.Task item, DateTime? begin)//adds a start date
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
                throw new BO.WrongOrderOfDatesException($"Task with id:{taskInList.Id} doesnt have scheduled date");
            if(task.ScheduledDate>= begin)
                throw new BO.WrongOrderOfDatesException("Order of dates is impossible");
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
        throw new WrongOrderOfDatesException("Impossible order of dates");
    }

    public int Create(BO.Task task)//creates a new task
    {

        int engineerId = task.Engineer is not null && Bl.GetProjectStatus() is Status.OnTrack && IsTaskCanBeAssigntToWorker(_bl.Engineer.Read(task.Engineer.Id)!, task)
            ? task.Engineer.Id : 0;

        if (Bl.GetProjectStatus() != Status.Unscheduled)
            throw  new ProjectStatusWrong("Cant add a task not during the schedule creation \n");
        if (task.ScheduledDate != null || task.DeadLineDate != null || task.StartDate != null)
        {
            throw new BlDataNotValidException("you can't add dates to the new task untill the schedule is finished. the system will add the new task without the dates\n");
        }
        if (task.Id != 0)
        {
            throw new BlDataNotValidException("you can't add task's ID  the system will add the new task with otomatic ID\n");
        }
        if (task.EngineerId != 0)
        {
            throw new BlDataNotValidException("you can't add engineer to the new task untill the schedule is finished. the system will add the new task without asigning it to the engineer\n");
        }
        DO.Task doTask = new DO.Task(task.Id, task.Alias, task.Description, task.CreatedAtDate,
            task.RequiredEffortTime, (DO.EngineerExperience)task.Complexity, task.Deliverables, 0,
            task.Remarks, null, null, null, false, null);
        try
        {
            int idTask = _dal.Task.Create(doTask);
            //AddOrUpdateDependencies(task);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex);
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

        return from DO.Task doTask in _dal.Task.ReadAll()
               where filter is null ? true : filter(Read(doTask.Id))
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
                   StartDate = doTask.StartDate,
                   TaskStatus = FindStatus(doTask),
                   Dependencies = FindDependencies(doTask),
                   Engineer = FindEngineer(doTask),
               };
    }
    public IEnumerable<BO.Task?> ReadAllTasksEngineerCanBeAssigned( int engineerId, Func<BO.Task, bool>? filter = null)//returns a list of tasks that matches the function
    {
       var engineer=_bl.Engineer.Read(engineerId);

        return from BO.Task boTask in _bl.Task.ReadAll()
               where filter is null ? true : filter(Read(boTask.Id))
               where IsTaskCanBeAssigntToWorker(engineer, boTask)
               select new BO.Task
               {
                   Id = boTask.Id,
                   Alias = boTask.Alias,
                   Description = boTask.Description,
                   CreatedAtDate = boTask.CreatedAtDate,
                   RequiredEffortTime = boTask.RequiredEffortTime,
                   Complexity = (BO.EngineerExperience)boTask.Complexity,
                   Deliverables = boTask.Deliverables,
                   EngineerId = boTask.EngineerId,
                   Remarks = boTask.Remarks,
                   ScheduledDate = boTask.ScheduledDate,
                   CompleteDate = boTask.CompleteDate,
                   DeadLineDate = boTask.DeadLineDate,
                   StartDate = boTask.StartDate,
                   TaskStatus = boTask.TaskStatus,
                   Dependencies = boTask.Dependencies,
                   Engineer = boTask.Engineer,
               };
    }
    
    public void AddDependency(BO.Task task,int depId)
    {
        BO.Task? dTask = _bl.Task.Read(depId);
        if (Bl.GetProjectStatus() != Status.Unscheduled)
            throw new ProjectStatusWrong("Cant add a dependency after schedule was created \n");
        foreach (var dep in task.Dependencies)//checking that the dependency doesnt already exist
        {
            if (dep.Id == depId)
                throw new BlAlreadyExistsException("dependency already exists");
        }
        if (dTask == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={depId} does Not exist");
        }
        else
        {

            DO.Dependency dep = new DO.Dependency()
            { 
                DependentTask=task.Id,
                DependOnTask=depId,
            };
            
            _dal.Dependency.Create(dep);

            BO.TaskInList taskDep = new BO.TaskInList
            {
                Id = depId,
                Alias = dTask.Alias,
                Description = dTask.Description,
                Status = dTask.TaskStatus
            };
            task.Dependencies!.Add(taskDep);

        }
    }
    private void AddOrUpdateDependencies(BO.Task task)
    {
        if (task.Dependencies is not null && task.Dependencies.Any())
        {
            foreach (var dependency in task.Dependencies)
            {
                _dal.Dependency!.Create(new Dependency(0, task.Id, dependency.Id));
            }
        }
    }
    
    public void Update(BO.Task task)// updates a task
    {
        var oldTask = _dal.Task.Read(task.Id);
        if (oldTask is null) throw new BO.BlDoesNotExistException($"The task doesnt exists");

        int engineerId = task.Engineer is not null && Bl.GetProjectStatus() is Status.OnTrack && IsTaskCanBeAssigntToWorker(_bl.Engineer.Read(task.Engineer.Id)!, task)
            ?task.Engineer.Id : oldTask.EngineerId;
        if (Bl.GetProjectStatus() != Status.OnTrack && task.EngineerId!=0)
            throw new ProjectStatusWrong("Cant assign an engineer to a task untill the schedule is finished\n");

        if (task.ScheduledDate != null || task.DeadLineDate != null || task.StartDate != null)
        {
            throw new BlDataNotValidException("you can't add dates to the task untill the schedule is finished. the system will add the new task without the dates\n");
        }

        DO.Task doTask = new DO.Task(task.Id, task.Alias, task.Description, task.CreatedAtDate, task.RequiredEffortTime,
            (DO.EngineerExperience)task.Complexity, task.Deliverables, engineerId, task.Remarks, 
            task.ScheduledDate, task.CompleteDate, task.DeadLineDate, false, task.StartDate);

        try
        {
          _dal.Task.Update(doTask);
         // AddOrUpdateDependencies(task);

        }
        catch (DO.DalAlreadyExistsException ex)
        {
           throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex);
        }
    }
}
