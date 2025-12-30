using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleData", menuName = "Battle Data")]
[Serializable]
public class BattleData : ScriptableObject
{
    [Tooltip(tooltip: "Figure Out if I should make keyNames unique")]
    public string keyName;
    public string[] enemyParty;
    public string[] playerParty;
    public string arenaID;
}
