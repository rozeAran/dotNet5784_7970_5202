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
    public IEnumerable<DateTime>? currentClock()
    {
        return new DateTime[] { Clock };
    }
    public IEnumerable<DateTime>? AddYearClock()
    {

        Clock.AddYears(1);
        return new DateTime[] { Clock };

    }
    public IEnumerable<DateTime>? AddDayClock()
    {
        Clock.AddDays(1);
        return new DateTime[] { Clock };
    }
    public IEnumerable<DateTime>? AddHourClock()
    {
        Clock.AddHours(1);
        return new DateTime[] { Clock };
    }


}

