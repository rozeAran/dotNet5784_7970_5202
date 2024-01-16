using Dal;
using DalApi;
using DO;
using System.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class DependencyImplementation: IDependency
{
    readonly string s_dependencies_xml = "dependencies";

    public int Create(Dependency item)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependencies_xml);
        int id = Config.NextTaskId;
        DO.Dependency copy = item with { Id = id };
        dependencies.Add(copy);
        XMLTools.SaveListToXMLSerializer(dependencies, s_dependencies_xml);
        return id;
    }

    public void Delete(int id)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependencies_xml);
        DO.Dependency dependencyToDelete = dependencies.FirstOrDefault(x => x.Id == id);

        if (dependencyToDelete != null)
        {
            dependencies.RemoveAll(x => x.Id == id);
            XMLTools.SaveListToXMLSerializer(dependencies, s_dependencies_xml);
        }
        else
        {
            XMLTools.SaveListToXMLSerializer(dependencies, s_dependencies_xml);
            throw new DalDeletionImpossible($"Task with ID={id} doesn't exist");
        }
    }

    public Dependency? Read(int id)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependencies_xml);
        DO.Dependency? dependency = dependencies.FirstOrDefault(x => x.Id == id);
        XMLTools.SaveListToXMLSerializer(dependencies, s_dependencies_xml);
        return dependency;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependencies_xml);
        var result = dependencies;
        DO.Dependency dependency = result.FirstOrDefault(filter);
        XMLTools.SaveListToXMLSerializer(dependencies, s_dependencies_xml);
        return dependency;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependencies_xml);

        if (filter != null)
        {
            IEnumerable<DO.Dependency?> dependency1 = from item in dependencies
                                                      where filter(item)
                                                      select item;
            XMLTools.SaveListToXMLSerializer(dependencies, s_dependencies_xml);
            return dependency1;
        }
        IEnumerable<DO.Dependency?> dependency2 = from item in dependencies
                                                  select item;
        XMLTools.SaveListToXMLSerializer(dependencies, s_dependencies_xml);
        return dependency2;
    }

    public void Update(Dependency item)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependencies_xml);
        if (dependencies.RemoveAll(it => it.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"task with id={item.Id} does not exist");
        dependencies.Add(item);
        XMLTools.SaveListToXMLSerializer(dependencies, s_dependencies_xml);
    }
}

