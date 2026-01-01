using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/Character Data")]
public class CharacterData : ScriptableObject
{
    
    public string keyName;
    //ID will be used for minifigs Read block 1, First Byte will be the ID 
    public int CharacterID;
    public string displayName;
    public string[] weaknesses;
    public string[] resists;
    public float HP;
    public float MP;
    public int level;
    public int XP;
    public int strength;
    public int defense;
    public int agility;
    public MovesetData[] moveset;
}