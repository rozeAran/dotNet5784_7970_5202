using System;

namespace BlImplementation;
using BlApi;
using BO;


internal class Bl : IBl
{
    
    private static DateTime s_Clock = DateTime.Now.Date;
    private static DalApi.IDal s_dal = DalApi.Factory.Get;

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

    public static Status GetProjectStatus()
    {
        if (s_dal.StartProjectDate is null) return Status.Unscheduled;
        if (s_dal.Task!.ReadAll().All(t => t.ScheduledDate is not null)) return Status.Scheduled;
        if(s_dal.Task!.ReadAll().Take(3).All(t => t.StartDate is not null)) return Status.OnTrack;
        throw new ProjectStatusWrong("Wrong Project Status");
    }

}

