using DO;

namespace BO;

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message, DO.DalAlreadyExistsException ex) : base(message) { }
}

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
}

[Serializable]
public class BlCantBeDeletedException : Exception
{
    public BlCantBeDeletedException(string? message) : base(message) { }
}

[Serializable]
public class BlCantBeUpdetedException : Exception
{
    public BlCantBeUpdetedException(string? message) : base(message) { }
}

[Serializable]
public class BlDataNotValidException : Exception
{
    public BlDataNotValidException(string? message) : base(message) { }
}