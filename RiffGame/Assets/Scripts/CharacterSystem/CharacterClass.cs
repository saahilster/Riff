using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class CharacterClass : MonoBehaviour
{
    TurnManager tracker;
    ActionPointHandler ap;
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
        ap = ActionPointHandler.instance;
        tracker = TurnManager.instance;
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
        LifeCheck();
    }

    private void OnTriggerStay(Collider other)
    {
        CharacterClass character = other.GetComponent<CharacterClass>();

        if (other.gameObject.layer == 8 && character != null)
        {
            if (tracker.currentPlayer == this && tracker.currentSkill == moveset.Any())
            {
                Debug.Log($"Move has been validated!");
                if (!tracker.attacking) return;

                Debug.Log("Attacking mode! Press ESC to cancel.");
                AttackCancel();

                if (Input.GetMouseButton(0))
                {
                    if (!tracker.attacking) return;

                    Debug.Log("Select a target.");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    int mask = 1 << 8;
                    if (Physics.Raycast(ray, out hit, 100f, mask))
                    {
                        if (hit.collider.gameObject)
                        {
                            Debug.Log("found target");
                            CharacterClass target = hit.collider.gameObject.GetComponent<CharacterClass>();
                            if (target == null)
                            {
                                Debug.Log("Target null");
                                return;
                            }

                            AttackTarget(target);
                        }
                    }
                }


            }
        }
    }

    public void AttackCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && tracker.attacking)
        {
            tracker.attacking = false;
            tracker.currentSkill = null;
            return;
        }
    }

    public void AttackTarget(CharacterClass target)
    {
        if (!tracker.attacking) return;

        if (target == null) return;

        int roll = DiceRoll();
        float statMultiplier = (str * 00.1f) - (target.def * 00.1f);

        if (statMultiplier <= 0)
        {
            Debug.Log("Defense is strong with this one..");
            statMultiplier = 1;
        }

        float damage = tracker.currentSkill.damage * (1 * statMultiplier);

        switch (roll)
        {
            case 1:
                Debug.Log(" Miss!");
                damage = 0;
                break;
            case 10:
                Debug.Log("Critical!");
                damage *= 1.5f;
                break;
            default:
                Debug.Log("Regular Damage");
                break;
        }

        if (ap.playerAP >= tracker.currentSkill.cost)
        {
            ap.playerAP -= tracker.currentSkill.cost;
            target.HP -= damage;
            Debug.Log(ap.actionPoints);
            Debug.Log($"Successfully finished AttackTarget function \n Target: {target.name}\n Hit by {tracker.currentPlayer} with {tracker.currentSkill} for {damage}");
            tracker.attacking = false;
            return;
        }
        else
        {
            Debug.Log("Not enough action points");
        }
    }

    public int DiceRoll()
    {
        int rollChance = Random.Range(1, 10);
        return rollChance;
    }

    public void LifeCheck()
    {
        if (HP <= 0 && isEnemy)
        {
            tracker.opps.Remove(this);
            Destroy(gameObject);
        }
        else if (HP <= 0 && !isEnemy)
        {
            tracker.party.Remove(this);
            Destroy(gameObject);
        }
    }
}

