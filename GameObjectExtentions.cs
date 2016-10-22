using UnityEngine;
using System;
using System.Collections;

public static class Extensions
{
    public static void Execute<T>(this GameObject target, Action<T> message)
    {
        T[] components = target.GetComponents<T>();
        
        foreach (T component in components)
        {
            message(component);
        }
    }

    public static T GetOrAdd<T>(this GameObject target) where T : Component{
        T c = target.GetComponent<T>();
        if (c == null)
            c = target.AddComponent<T>();
        return c;
    }
}