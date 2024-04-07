﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PL.Gantt.GanttChart
{
    public class TimeLine
    {
        public TimeLine()
        {
            Items = new ObservableCollection<TimeLineItem>();
        }

        public ObservableCollection<TimeLineItem> Items { get; set; }

    }
}
