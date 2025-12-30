using UnityEngine;

[CreateAssetMenu(fileName = "ArenaData", menuName = "Scriptable Objects/Arena Data")]
public class ArenaData : ScriptableObject
{
    public string arenaName;
    public GameObject arenaPrefab;
}
