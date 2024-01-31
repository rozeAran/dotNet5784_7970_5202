﻿using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();


}
