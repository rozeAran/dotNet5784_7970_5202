using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
namespace Dal;

internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextDepId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDepId"); }

    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }

    public static DateTime? StartProjectDate
    {
        get => XMLTools.ToDateTimeNullable(XMLTools.LoadListFromXMLElement(s_data_config_xml),
                nameof(StartProjectDate));

        set
        {
            var root = XMLTools.LoadListFromXMLElement(s_data_config_xml);
            root.Element(nameof(StartProjectDate))!.Value
                = value.ToString()!;

            XMLTools.SaveListToXMLElement(root, s_data_config_xml);
        }
    }
    public static DateTime? EndProjectDate
    {
        get => XMLTools.ToDateTimeNullable(XMLTools.LoadListFromXMLElement(s_data_config_xml),
                nameof(EndProjectDate));

        set
        {
            var root = XMLTools.LoadListFromXMLElement(s_data_config_xml);
            root.Element(nameof(StartProjectDate))!.Value
                = value.ToString()!;

            XMLTools.SaveListToXMLElement(root, s_data_config_xml);
        }
    }

}
