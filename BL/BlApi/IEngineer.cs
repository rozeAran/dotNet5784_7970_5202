namespace BlApi;
/// <summary>
/// the interface of engineer in the logical layer 
/// </summary>
/// <method name=""> 
/// <method name="create"> : trys to add a Engineer to the data layer</method>
/// <method name="read">: returns the Engineer that matches the id </method>
/// <method name="readAll">: returns a list of Engineers that matches the function </method>
/// <method name="update">: if the data is valid, will try to update the Engineer in the data layer </method>
/// <method name="delete">: if the Engineer is deletebul then will delete it </method>
public interface IEngineer
{
    public int Create(BO.Engineer item);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null);
    public void Update(BO.Engineer item);
    public void Delete(int id);
}
