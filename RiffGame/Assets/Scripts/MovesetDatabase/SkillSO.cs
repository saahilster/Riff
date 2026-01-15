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
    public Sprite icon;
    public Sprite art;

    //"under the hood" data
    public float damage;
    public string type;  
    //Default values for these will be 1. buffs in this game will work by multiplying the user's stats
    public float strMultiplier;
    public float defMultiplier;
    
    //Cost for the move
    public int cost;
    //Have to roll this for it to work.
    public int rollReq;
    //Try to make a range system for moves.
    public float rangeRadius;
    public float attackRadius;
    public float duration;
}
