using UnityEngine;
using UnityEngine.UIElements;

public class CharacterClass
{
    private float HP;
    private float MP;
    private int _level;
    private SkillSO[] _moveset;
    private string[] _weaknesses;
    private string[] _strengths;
    private int _str;
    private int _def;

    public CharacterClass(
        float hp, 
        float mp, 
        int level, 
        SkillSO[] moveset, 
        string[] weaknesses,
        string[] strengths,
        int def,
        int str
        )
    {
       hp = HP;
       mp = MP;
       level = _level;
       moveset = _moveset;
       weaknesses = _weaknesses;
       strengths = _strengths;
       str = _str;
       def = _def; 
    }

    public void UseMove()
    {
        
    }

    public void BasicAttack()
    {
        
    }

    public void Gaurd()
    {
        
    }
}
