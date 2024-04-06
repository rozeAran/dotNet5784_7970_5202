using BO;

namespace BlApi;
public interface IBl
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public DateTime Clock { get;}
    public ITask Task { get; }
    public IEngineer Engineer { get; }

    public void InitializeDB()=>DalTest.Initialization.Do();
    public void ResetDB() => DalTest.Initialization.Reset();
    public void InitializeClock();
    /* public IEnumerable<DateTime>? currentClock();
     public IEnumerable<DateTime>? AddYearClock();
     public IEnumerable<DateTime>? AddDayClock();
     public IEnumerable<DateTime>? AddHourClock();*/
    public DateTime? currentClock();
    public DateTime? AddYearClock();
    public DateTime? AddDayClock();
    public DateTime? AddHourClock();



}
