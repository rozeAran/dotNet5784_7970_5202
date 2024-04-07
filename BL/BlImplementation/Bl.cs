using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;
using BlApi;

internal class Bl : IBl
{
    
    private static DateTime s_Clock = DateTime.Now.Date;
    private DalApi.IDal s_dal = DalApi.Factory.Get;

    public DateTime? StartProjectDate
    {
        get => s_dal.StartProjectDate;
        set => s_dal.StartProjectDate = value;
    }
    public DateTime? EndProjectDate
    {
        get => s_dal.EndProjectDate;
        set => s_dal.EndProjectDate = value;
    }
    public DateTime Clock { get { return s_Clock; } private set { s_Clock = value; } }

   // public int schedule = -1;
    public ITask Task => new TaskImplementation(this);
    public IEngineer Engineer => new EngineerImplementation(this);


    public void InitializeDB() => DalTest.Initialization.Do();
    public void ResetDB() => DalTest.Initialization.Reset();

    public void InitializeClock()
    {
        Clock = DateTime.Now;
    }

    public DateTime? CurrentClock()
    {
        return Clock;
    }
    public DateTime? AddYearClock()
    {

        return Clock.AddYears(1);

    }
    public DateTime? AddDayClock()
    {
        
        return Clock.AddDays(1);
    }
    public DateTime? AddHourClock()
    {
        return Clock.AddHours(1);
    }


}

