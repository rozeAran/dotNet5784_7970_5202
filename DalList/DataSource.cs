namespace Dal;

static class DataSource
{
    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static class Config
    {
        internal const int startDepId = 1000;
        private static int nextDepId = startDepId;
        internal static int NextDepId { get => nextDepId++; }
        //...
    }
    internal static class Config
    {
        internal const int startTaskId = 1000;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
        //...
    }

}
