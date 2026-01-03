using UnityEngine;


//Data serves only to create the character NOT to track data through the game.
[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/Character Data")]
public class CharacterData : ScriptableObject
{
    //ID will be used for minifigs Read block 1, First Byte will be the ID 
    public int CharacterID;
    //UI
    public string keyName;
    
    public string displayName;
    public Sprite icon;

    public string[] weaknesses;
    public string[] resists;
    public float HP;
    public float MP;

    //Don't worry too much about the leveling system
    public int level;
    public int strength;
    public int defense;
    public SkillSO[] moveset;
    
    //For spawning
    public GameObject battlePrefab;
    //party prefab only meant to be used for party members in non battle encounters.
    public GameObject partyPrefab;
}