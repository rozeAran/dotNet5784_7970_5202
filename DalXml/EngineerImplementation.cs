using Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineers_xml = "engineers";
    static Engineer GetEngineer(XElement s)
    {
        return new Engineer()
        {
            Id = s.ToIntNullable("Id") ?? throw new FormatException("cant convert id"),
            Name = (string?)s.Element("Name") ?? "",
            Email = (string?)s.Element("Email") ?? "",
            Cost = s.ToDoubleNullable("Cost") ?? throw new FormatException("cost is not valid"),
            Level = s.ToEnumNullable<EngineerExperience>("engineer level") ?? EngineerExperience.Beginner,

        };

    }

    public int Create(Engineer item)
    {

        XElement? engineerElements = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        //int id = Config.NextTaskId;
        Engineer copy = item with { Id = item.Id };

        // Check if an engineer with the same Id already exists
        if (Read(item.Id)!=null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        engineerElements.SetValue(copy);
        XMLTools.SaveListToXMLElement(engineerElements, s_engineers_xml);
        return item.Id;

    }

    public void Delete(int id)
    {

        //XElement? engineerElements = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        //Engineer engineerToDelete = Read(id);
        if (Read(id)!=null)
        {

            XElement? engineerElements = XMLTools.LoadListFromXMLElement(s_engineers_xml);
            XElement? engineerElem = engineerElements.Elements().FirstOrDefault(en => (int?)en.Element("Id") == id);
            engineerElem.Remove();
            XMLTools.SaveListToXMLElement(engineerElem, s_engineers_xml);

        }
        else
        {
            //XMLTools.SaveListToXMLElement(engineerElem, s_engineers_xml);
            throw new DalDeletionImpossible($"Engineer with ID={id} doesn't exist");
        }
        

    }

    public Engineer? Read(int id)
    {
        XElement? engineerElem=XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().FirstOrDefault(en=>(int?)en.Element("Id")==id);
        return engineerElem is null ? null : GetEngineer(engineerElem);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().Select(s=>GetEngineer(s)).FirstOrDefault();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if(filter == null)
        {
            return XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().Select(s => GetEngineer(s));
        }
        else
        {
            return XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().Select(s => GetEngineer(s)).Where(filter);

        }
    }

    public void Update(Engineer item)
    {
        XElement? engineerElem = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        if(engineerElem.Elements().FirstOrDefault(en => (int?)en.Element("Id") == item.Id)==null)
            throw new DalDoesNotExistException($"Engineer with id={item.Id} does not exist");
        engineerElem.Elements().FirstOrDefault(en => (int?)en.Element("Id") == item.Id).Remove();
        engineerElem.Add(item);
        XMLTools.SaveListToXMLElement(engineerElem, s_engineers_xml);

    }

    public void DeleteAll()
    {
        XElement? dependencies = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        dependencies.RemoveAll();
        XMLTools.SaveListToXMLElement(dependencies, s_engineers_xml);
    }
}
