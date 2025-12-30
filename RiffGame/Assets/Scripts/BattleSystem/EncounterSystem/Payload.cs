using System.Collections.Generic;
using NUnit.Framework;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Payload
{
    public static Dictionary<string, string> payloads = new();

    public static void Set(string key, string json)
    {
        payloads[key] = json;
    }

    public static bool Has(string key)
    {
        return payloads.ContainsKey(key);
    }

    public static string Consume(string key)
    {
        if (!Has(key))
        {
            return null;
        }

        string json = payloads[key];
        payloads.Remove(key);
        return json;
    }
}
