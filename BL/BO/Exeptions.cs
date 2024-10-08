﻿using DO;

namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception//the object with this id doesn exsist
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
}


[Serializable]
public class BlAlreadyExistsException : Exception//the object withe this id already exsists
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string? message, Exception innerException) : base(message) { }
}

[Serializable]
public class BlCantBeDeletedException : Exception// its impossible to delete this object- not allowed
{
    public BlCantBeDeletedException(string? message) : base(message) { }
    public BlCantBeDeletedException(string? message, Exception innerException) : base(message) { }
}

[Serializable]
public class BlCantBeUpdetedException : Exception//this item cant be updated
{
    public BlCantBeUpdetedException(string? message) : base(message) { }
    public BlCantBeUpdetedException(string? message, Exception innerException) : base(message) { }
}

[Serializable]
public class BlDataNotValidException : Exception// given value is invalid
{
    public BlDataNotValidException(string? message) : base(message) { }
    public BlDataNotValidException(string? message, Exception innerException) : base(message) { }
}

[Serializable]
public class BlNotAPossabilityException : Exception//there is no such an option
{
    public BlNotAPossabilityException(string? message) : base(message) { }
    public BlNotAPossabilityException(string? message, Exception innerException) : base(message) { }
}

[Serializable]
public class BlXMLFileLoadCreateException : Exception//something with the xml file went wrong
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    public BlXMLFileLoadCreateException(string? message, Exception innerException) : base(message) { }
}

[Serializable]
public class WrongOrderOfDatesException : Exception// order of dates doesnt make sense
{
    public WrongOrderOfDatesException(string? message) : base(message) { }
}

[Serializable]
public class BlNullPropertyException : Exception//this item is null therfor the program cant operate on it
{
    public BlNullPropertyException(string? message) : base(message) { }
}

[Serializable]
public class StartTimeOfDependenceTaskNotExist : Exception//There is no start date to the dependent task
{
    public StartTimeOfDependenceTaskNotExist(string? message) : base(message) { }
}

[Serializable]
public class ProjectStatusWrong : Exception//There is no start date to the dependent task
{
    public ProjectStatusWrong(string? message) : base(message) { }
}

