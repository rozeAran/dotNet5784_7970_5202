using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

[Serializable]
public class DalDoesNotExistException : Exception//the object with this id doesn exsist
{
    public DalDoesNotExistException(string? message) : base(message) { }
}


[Serializable]
public class DalAlreadyExistsException : Exception//the object withe this id already exsists
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}


[Serializable]
public class DalDeletionImpossible : Exception// its impossible to delete this object- not allowed
{
    public DalDeletionImpossible(string? message) : base(message) { }
}