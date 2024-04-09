using System.Reflection;


namespace BO;

static class Tools
{
    public static string ToStringProperty <T>(this T t)//tostring method for all the objects
    {
        string str = "";
        foreach(PropertyInfo item in t.GetType().GetProperties())
        { 
            str += "\n"+item.Name+ ": "+item.GetValue(t,null);
        }
        return str;
    }

   
}
