using DalApi;
using Dal;
using DO;
using System.Xml.Serialization;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
namespace Dal;

internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";

    public int Create(DO.Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        int id = Config.NextTaskId;
        DO.Task copy = item with { Id = id };
        tasks.Add(copy);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        return id;

    }

    public void Delete(int id)
    {

        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        DO.Task taskToDelete = tasks.FirstOrDefault(x => x.Id == id);

        if (taskToDelete != null)
        {
            tasks.RemoveAll(x => x.Id == id);
            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        }
        else
        {
            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
            throw new DalDeletionImpossible($"Task with ID={id} doesn't exist");
        }
    }

    public DO.Task? Read(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        DO.Task? task = tasks.FirstOrDefault(x => x.Id == id);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        return task;
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        var result = tasks;
        DO.Task task = result.FirstOrDefault(filter);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        return task;
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

        if (filter != null)
        {
            IEnumerable<DO.Task?> task1 = from item in tasks
                                         where filter(item)
                                         select item;
            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
            return task1;
        }
        IEnumerable<DO.Task?> task2= from item in tasks
                                    select item;
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        return task2;

    }

    public void Update(DO.Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        if (tasks.RemoveAll(it => it.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"task with id={item.Id} does not exist");
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }

    public void DeleteAll()
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        tasks.Clear();
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }
}
