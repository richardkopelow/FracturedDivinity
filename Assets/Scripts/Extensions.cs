using System;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GetComponentUpwardSearch<T>(this GameObject go) where T : Component
    {
        return go.GetComponent<Transform>().GetComponentUpwardSearch<T>();
    }
    public static T GetComponentUpwardSearch<T>(this Transform tr) where T : Component
    {
        Transform trans = tr;
        T com = trans.GetComponent<T>();
        while (com==null&&trans!=null)
        {
            trans = trans.parent;
            com = trans.GetComponent<T>();
        }
        return com;
    }

}
