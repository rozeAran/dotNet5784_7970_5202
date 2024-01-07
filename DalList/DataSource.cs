﻿namespace Dal;

static class DataSource
{
    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static class Config
    {
        //running number dependency::id
        internal const int startDepId = 1;
        private static int nextDepId = startDepId;
        internal static int NextDepId { get => nextDepId++; }

        //running number task::id
        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
    }

}
