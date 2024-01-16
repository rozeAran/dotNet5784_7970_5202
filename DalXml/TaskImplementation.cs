using DalApi;
using Dal;
using DO;
using System.Xml.Serialization;
using System.Data.Common;
using System.IO;
namespace Dal;

internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";

    public int Create(DO.Task item)
    {
        int id = Config.NextTaskId;
        DO.Task copy = item with { Id = id };
        List<DO.Task> tasks = new List<DO.Task>();
        tasks= LoadListFromXMLSerializer(s_tasks_xml);
        //FileStream fs = new FileStream(path, FileMode.Open);
        XmlSerializer x = new XmlSerializer(typeof(List<DO.Task>));
         (List<DO.Task>)x.Deserialize(s_tasks_xml);


        XmlSerializer Deserialize = new XmlSerializer(typeof(DO.Task));
        if (tasks.Count > 0 )
             tasks.Deserialize(s_tasks_xml);
        tasks.Add(item);
        throw new NotImplementedException();
        DO.Task copy = item with { Id = item.Id};

        // יצירת אינסטנס של XmlSerializer עבור המחלקה Task
        XmlSerializer serializer = new XmlSerializer(typeof(Task));

        // יצירת Stream חדש עבור הכתיבה לקובץ
        using (StreamWriter writer = new StreamWriter("tasks.xml"))
        {
            // הכתיבה של המשימה לתוך הקובץ באמצעות XmlSerializer
            serializer.Serialize(writer, copy);
        }

        DataSource.Tasks.Add(copy);
        return copy.Id;
        /*        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return item.Id;*/
    }

    public void Delete(int id)
    {
      
        throw new NotImplementedException();
    }

    public DO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Task item)
    {
        throw new NotImplementedException();
    }
}
