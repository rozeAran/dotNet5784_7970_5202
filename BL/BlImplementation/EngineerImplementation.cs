using BlApi;
using BO;
using DO;
using System.Linq;

namespace BlImplementation;
/// <summary>
/// the interface of engineer in the logical layer 
/// </summary>
/// <method name="findTask"> :find the task that the engineer is working on
/// <method name="create"> : trys to add a Engineer to the data layer</method>
/// <method name="read">: returns the Engineer that matches the id </method>
/// <method name="readAll">: returns a list of Engineers that matches the function </method>
/// <method name="update">: if the data is valid, will try to update the Engineer in the data layer </method>
/// <method name="delete">: if the Engineer is deletebul then will delete it </method>
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer item)
    {

        try
        {
            if (item.Id <= 0 || item.Name == "" || item.Cost <= 0 || !(item.Email.Contains(" ")) || item.Email.Contains("@") || item.Email.Contains(".co"))
            {
                throw new BlDataNotValidException("data is not valid\n");
            }
            DO.Engineer doEngineer = new DO.Engineer (item.Id, item.Name, item.Email, (DO.EngineerExperience/*?*/)item.Level, item.Cost);
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={item.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        BO.Engineer? temp = Read(id);
        if (temp.Tasks==null) 
        {
            throw new BlCantBeDeletedException($"Engineer with ID={id} cant be deleted");
        }
        _dal.Engineer.Delete(id);
    }

    public BO.Engineer? Read(int id)
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
            Tasks = FindTask(id)//adding the current task that the engineer is workng on
        };
    }



    public IEnumerable<EngineerInTask> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.EngineerInTask
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                });
    }


    public void Update(BO.Engineer item)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(item.Id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={item.Id} does Not exist");
        if ((int)item.Level < (int)doEngineer.Level)
        {
            throw new BlCantBeUpdetedException($"Engineer with ID={item.Id} cant be updated");
        }
        if (item.Id <= 0 || item.Name == "" || item.Cost <= 0 || !(item.Email.Contains(" ")) || item.Email.Contains("@") || item.Email.Contains(".co"))
        {
            throw new BlDataNotValidException("data is not valid\n");
        }
        BO.Engineer? boEngineer = Read(item.Id);
        if (item.Tasks != boEngineer.Tasks)
        {
            DO.Task newTask= _dal.Task.Read(item.Tasks.Id);
            DO.Task temp = new DO.Task
            {
                Id = newTask.Id,
                Alias = newTask.Alias,
                Description = newTask.Description,
                CreatedAtDate = newTask.CreatedAtDate,
                RequiredEffortTime = newTask.RequiredEffortTime,
                Complexity = newTask.Complexity,
                Deliverables = newTask.Deliverables,
                EngineerId = item.Id,
                Remarks = newTask.Remarks,
                ScheduledDate = newTask.ScheduledDate,
                CompleteDate = newTask.CompleteDate,
                DeadLineDate = newTask.DeadLineDate,
                StartDate = newTask.StartDate
            };
            _dal.Task.Update(temp);
            //updet in engineer in task
        }
        _dal.Engineer.Update(doEngineer);
    }

    public BO.TaskInEngineer FindTask(int id)
    {
        if (_dal.Task.Read(id) == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        return ((TaskInEngineer)(from DO.Task task in _dal.Task.ReadAll()
                where task.Id == id
                select new BO.TaskInEngineer
                {
                    Id = task.Id,
                    Alias = task.Alias,
                }));

    }
}
