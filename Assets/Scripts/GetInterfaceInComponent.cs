using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GetInterfaceInComponent
{
    public static T Invoke<T>(Transform t) where T : class
    {
        foreach (var i in t.GetComponents<MonoBehaviour>())
        {
            if (i is T)
            {
                return (T)(object)i;
            }
        }
        return null;
    }
}
