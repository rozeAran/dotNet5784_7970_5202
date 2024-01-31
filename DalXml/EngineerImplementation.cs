using Dal;
using DalApi;
using DO;
using System;//.Data.Common;
using System.Reflection.Emit;
using System.Xml.Linq;


namespace Dal;

internal class EngineerImplementation : IEngineer
{
    XElement? EngineerRoot;
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

        /*XElement engineerElements = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        XElement newItem = EngineerToXelement(item with { Id = item.Id });
        if (Read(item.Id)!=null)// Check if an engineer with the same Id already exists
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        engineerElements.Add(newItem);
        XMLTools.SaveListToXMLElement(engineerElements, s_engineers_xml);
        return item.Id;*/
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineers_xml);
        DO.Engineer copy = item with { Id = item.Id };
        engineers.Add(copy);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
        return item.Id;


    }

    static XElement EngineerToXelement(Engineer item)
    {
        return new XElement("Engineer",
            new XElement("Id", item.Id),
            new XElement("name", item.Name),
            new XElement("email", item.Email),
            new XElement("level", item.Level),
            new XElement("cost", item.Cost)
            ) ; 
    }


    public void Delete(int id)
    {

        /*if (Read(id)!=null)
        {

             XElement? engineerElements = XMLTools.LoadListFromXMLElement(s_engineers_xml);
             XElement? engineerElem = engineerElements.Elements().FirstOrDefault(en => (int?)en.Element("Id") == id);
             engineerElem!.Remove();
             XMLTools.SaveListToXMLElement(engineerElem, s_engineers_xml);

        }
        else
        {
            throw new DalDeletionImpossible($"Engineer with ID={id} doesn't exist");
        }*/
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineers_xml);
        DO.Engineer engineerToDelete = engineers.FirstOrDefault(x => x.Id == id);

        if (engineerToDelete != null)
        {
            engineers.RemoveAll(x => x.Id == id);
            XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
        }
        else
        {
            XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
            throw new DalDeletionImpossible($"engineer with ID={id} doesn't exist");
        }

    }

    public Engineer? Read(int id)
    {
        /*XElement? engineerElem=XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().FirstOrDefault(en=>(int?)en.Element("Id")==id);
        Engineer? temp= engineerElem is null ? null : GetEngineer(engineerElem);
        XMLTools.SaveListToXMLElement(engineerElem, s_engineers_xml);
        return temp;*/
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineers_xml);
        DO.Engineer? engineer = engineers.FirstOrDefault(x => x.Id == id);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
        return engineer;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        //return XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().Select(s=>GetEngineer(s)).FirstOrDefault();
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineers_xml);
        var result = engineers;
        DO.Engineer engineer = result.FirstOrDefault(filter);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
        return engineer;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        /*if(filter == null)
        {
            return XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().Select(s => GetEngineer(s));
        }
        else
        {
            return XMLTools.LoadListFromXMLElement(s_engineers_xml).Elements().Select(s => GetEngineer(s)).Where(filter);

        }*/
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineers_xml);

        if (filter != null)
        {
            IEnumerable<DO.Engineer?> engineer1 = from item in engineers
                                                      where filter(item)
                                                      select item;
            XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
            return engineer1;
        }
        IEnumerable<DO.Engineer?> engineer2 = from item in engineers
                                                  select item;
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
        return engineer2;
    }

    public void Update(Engineer item)
    {
        /*XElement? engineerElem = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        if(engineerElem.Elements().FirstOrDefault(en => (int?)en.Element("Id") == item.Id)==null)
            throw new DalDoesNotExistException($"Engineer with id={item.Id} does not exist");
        engineerElem.Elements().FirstOrDefault(en => (int?)en.Element("Id") == item.Id).Remove();
        engineerElem.Add(item);
        XMLTools.SaveListToXMLElement(engineerElem, s_engineers_xml);*/
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineers_xml);
        if (engineers.RemoveAll(it => it.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"Engineer with id={item.Id} does not exist");
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);

    }

    public void DeleteAll()
    {
        /*XElement? dependencies = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        dependencies.RemoveAll();
        XMLTools.SaveListToXMLElement(dependencies, s_engineers_xml);*/
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineers_xml);
        engineers.Clear();
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
    }
}
