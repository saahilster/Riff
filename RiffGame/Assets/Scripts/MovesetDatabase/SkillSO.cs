using UnityEngine;

public enum TargetType
{
   Single,
   Group,
   All 
}

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Objects/SkillSO")]
public class SkillSO : ScriptableObject
{
    //ID and player help
    //Remember to parse the id do not use it as a string.
    public int keyID;
    public string moveName;
    public string description;

    //"under the hood" data
    public float damage;
    public string type;    
}
