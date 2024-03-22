using BO;

namespace BlApi;
public interface IBl
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public DateTime Clock {  get;}
    public ITask Task { get; }
    public IEngineer Engineer { get; }

    public void InitializeDB()=>DalTest.Initialization.Do();

    public void ResetDB() => DalTest.Initialization.Reset();
    public void InitializeClock();
    public void AddYearClock();
    public void AddDayClock();
    public void AddHourClock();



}
