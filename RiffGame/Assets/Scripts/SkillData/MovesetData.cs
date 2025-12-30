using UnityEngine;

[CreateAssetMenu(fileName = "MovesetData", menuName = "Moveset Data")]
public class MovesetData : ScriptableObject
{
    public string keyName;
    public string moveName;
    public float MPCost;
    public float HPCost;
    public float damage;
    public string moveType;
}
