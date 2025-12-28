using UnityEngine;

public static class PracticeSerializer
{
    public static string SerializeData(PracticeData data)
    {
        return JsonUtility.ToJson(data);
    }

    public static PracticeData DeserializeData(string json)
    {
        return JsonUtility.FromJson<PracticeData>(json);
    }
}
