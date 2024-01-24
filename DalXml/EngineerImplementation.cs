﻿using Dal;
using DalApi;
using DO;
using System;//.Data.Common;
using System.Xml.Linq;


namespace Dal;

/*
internal class DependenceImplementation : IDependence
{
   
    public void Delete(int id)
    {
        try
        {
            if (id == 0) { DeleteFiles(); }
            else
            {
                XElement? dependenenceElement;
                if (dependenenceRoot == null) { throw new DalIsNullException("xelement was null"); }
                dependenenceElement = (from p in dependenenceRoot!.Elements()
                                       where Convert.ToInt32(p.Element("id")!.Value) == id
                                       select p).FirstOrDefault();
                dependenenceElement!.Remove();
                dependenenceElement.Save(dependenencePath);
            }
        }
        catch
        { }
    }

    public Dependence? Read(int id)
    {
        LoadData();
        Dependence? dependenence;
        try
        {
            if (dependenenceRoot == null) { throw new(); }
            dependenence = (from l in dependenenceRoot.Elements()
                            where Convert.ToInt32(l.Element("id")!.Value) == id
                            select new Dependence()
                            {
                                Id = Convert.ToInt32(l.Element("id")!.Value),
                                DependenentTask = Convert.ToInt32(l.Element("dependenentTask")!.Value),
                                DependsOnTask = Convert.ToInt32(l.Element("dependsOnTask")!.Value)
                            }).FirstOrDefault();
        }
        catch
        {
            dependenence = null;
        }
        return dependenence;
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        LoadData();
        List<Dependence?>? dependenences = null;
        Dependence? dependenence = null;
        try
        {
            if (dependenenceRoot == null) { throw new(); }
            foreach (var l in dependenenceRoot.Elements())
            {
                dependenence = new Dependence()
                {
                    Id = Convert.ToInt32(l.Element("id")!.Value),
                    DependenentTask = Convert.ToInt32(l.Element("dependenentTask")!.Value),
                    DependsOnTask = Convert.ToInt32(l.Element("dependsOnTask")!.Value)
                };
                dependenences!.Add(dependenence);
            }
            foreach (var dependenence1 in dependenences!)
            {
                if (filter(dependenence1!))
                    dependenence = dependenence1;
            }
        }
        catch
        {
            dependenence = null;
        }
        return dependenence;
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        LoadData();
        List<Dependence?> dependenences;
        try
        {
            if (dependenenceRoot == null) { throw new(); }
            dependenences = (from l in dependenenceRoot.Elements()
                             select new Dependence()
                             {
                                 Id = Convert.ToInt32(l.Element("id")!.Value),
                                 DependenentTask = Convert.ToInt32(l.Element("dependenentTask")!.Value),
                                 DependsOnTask = Convert.ToInt32(l.Element("dependsOnTask")!.Value)
                             }).ToList();
        }
        catch
        {
            dependenences = null;
        }
        return dependenences;
    }

    public void UpDate(Dependence item)
    {
        XElement? dependenenceElement;
        if (dependenenceRoot == null) { throw new DalIsNullException("xelement was null"); }
        dependenenceElement = (from p in dependenenceRoot!.Elements()
                               where Convert.ToInt32(p.Element("id")!.Value) == item.Id
                               select p).FirstOrDefault();
        dependenenceElement.Element("dependenentTask")!.Value = item.DependenentTask.ToString();
        dependenenceElement.Element("dependsOnTask")!.Value = item.DependsOnTask.ToString();
        dependenenceElement.Save(dependenencePath);
    }
}
*/
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

        XElement engineerElements = XMLTools.LoadListFromXMLElement(s_engineers_xml);
        Engineer copy = item with { Id = item.Id };
        if (Read(item.Id)!=null)// Check if an engineer with the same Id already exists
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
             engineerElem!.Remove();
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
