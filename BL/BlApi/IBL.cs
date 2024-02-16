using BO;

namespace BlApi;
public interface IBl
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public ITask Task { get; }
    public IEngineer Engineer { get; }

    public void InisializeDB()=>DalTest.Initialization.Do();

    public void ResetDB() => DalTest.Initialization.Reset();
}
