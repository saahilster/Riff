using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    public string keyName;
    public string enemyName;
    public string[] weaknesses;
    public string[] resists;
    public float HP;
    public float MP;
    public int level;
    public int strength;
    public int defense;
    public int agility;
    public MovesetData[] moveset;
}
