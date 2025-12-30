using UnityEngine;

[CreateAssetMenu(fileName = "ArenaData", menuName = "Scriptable Objects/ArenaData")]
public class ArenaData : ScriptableObject
{
    public string arenaName;
    public GameObject prefab;
}
