using UnityEngine;

[CreateAssetMenu(fileName = "BattleData", menuName = "Scriptable Objects/Battle Data")]
public class BattleData : ScriptableObject
{
    public string keyName;
    public bool Boss;
    public CharacterData[] enemyParty;
    public string arenaID;
}
