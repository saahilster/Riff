using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public static class EncounterCollector
{
    public static Dictionary<string, BattleData> battleDatabase = new();

    public static void RegisterData(string key, BattleData data)
    {
        battleDatabase[key] = data;
    }

    public static bool Has(string key)
    {
        return battleDatabase.ContainsKey(key);
    }

    public static BattleData Consume(string key)
    {
        if (!battleDatabase.TryGetValue(key, out var data))
            return null;
        
        battleDatabase.Remove(key);
        return data;
    }
}
