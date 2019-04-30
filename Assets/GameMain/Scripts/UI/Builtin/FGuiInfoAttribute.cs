using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trinity
{
    public class FGuiInfoAttribute : Attribute
    {
        public string PackageName;
        public string ComponentName;
        public FGuiInfoAttribute(string packageName)
        {
            PackageName = packageName;
        }
        public FGuiInfoAttribute(string packageName,string componentName)
        {
            PackageName = packageName;
            ComponentName = componentName;
        }

    }

}
