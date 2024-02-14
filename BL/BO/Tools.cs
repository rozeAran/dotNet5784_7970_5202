﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
