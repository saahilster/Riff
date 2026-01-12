using UnityEngine;
using UnityEngine.UIElements;

public class CharacterClass : MonoBehaviour
{
    [SerializeField] public CharacterData data;
    public float HP;
    public float MP;
    private int level;
    private SkillSO[] moveset;
    private string[] weaknesses;
    private string[] resists;
    public int str;
    public int def;
    public bool isEnemy;

    private void Start()
    {
        HP = data.HP;
        MP = data.MP;
        level = data.level;
        moveset = data.moveset;
        weaknesses = data.weaknesses;
        resists = data.resists;
        str = data.strength;
        def = data.defense;
    }

    private void Update()
    {

    }
}
