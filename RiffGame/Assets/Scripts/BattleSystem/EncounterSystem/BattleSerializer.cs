using UnityEngine;

public static class BattleSerializer
{
    public static string SerializeData(BattleData data)
    {
        return JsonUtility.ToJson(data);
    } 

    public static BattleData DeserializeData(string json)
    {
        return JsonUtility.FromJson<BattleData>(json);
    }
}
