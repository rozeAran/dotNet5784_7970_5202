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
    public ITask Task => new TaskImplementation(this);

    public IEngineer Engineer => new EngineerImplementation(this);

    public void InitializeDB() => DalTest.Initialization.Do();
    public void ResetDB() => DalTest.Initialization.Reset();

    public void InitializeClock()
    {
        Clock = DateTime.Now;
    }
    public void AddYearClock()
    {
        Clock.Year=Clock.Year+1;
    }
    public void AddDayClock()
    {
        Clock.Day = Clock.Day + 1;
    }
    public void AddHourClock()
    {
        Clock.Hour = Clock.Hour + 1;
    }


}

