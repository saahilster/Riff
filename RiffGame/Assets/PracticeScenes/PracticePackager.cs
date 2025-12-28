using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PracticePackager
{
    public static Dictionary<string, string> packages = new();

    public static void Set(string key, string json)
    {
        packages[key] = json;
    }

    public static string Consume(string key)
    {
        if (!packages.ContainsKey(key))
        {
            return null;
        }

        string value = packages[key];
        packages.Remove(key);
        return value;
    }

    public static bool Has(string key)
    {
        return packages.ContainsKey(key);
    }
}
