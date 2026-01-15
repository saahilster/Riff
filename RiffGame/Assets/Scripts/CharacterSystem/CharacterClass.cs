using UnityEngine;
using UnityEngine.UIElements;

public class CharacterClass : MonoBehaviour
{
    [SerializeField] BattleTracker tracker;
    [SerializeField] public CharacterData data;
    SphereCollider rangeRadius;
    public float HP;
    public float MP;
    public int level;
    public SkillSO[] moveset;
    public string[] weaknesses;
    public string[] resists;
    public int str;
    public int def;
    public bool isEnemy;
    public SkillSO currentMove;

    private void Start()
    {
        rangeRadius = GetComponent<SphereCollider>();
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

    private void OnTriggerStay(Collider other)
    {
        CharacterClass character = other.GetComponent<CharacterClass>();

        if (other.gameObject.layer == 8 && character != null)
        {
            Debug.Log(other.gameObject.name);
        }
    }
}
