using BlApi;
using BO;
using System.Linq;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(Engineer item)
    {

        try
        {
            if (item.Id <= 0 || item.Name == "" || item.Cost <= 0 || !(item.Email.Contains(" ")) || item.Email.Contains("@") || item.Email.Contains(".co"))
            {
                throw new Exception("data is not valid\n");
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
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = null//adding the current task that the engineer is workng on
        };
    }



    public IEnumerable<EngineerInTask> ReadAll(Func<Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.EngineerInTask
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                });
    }


    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
