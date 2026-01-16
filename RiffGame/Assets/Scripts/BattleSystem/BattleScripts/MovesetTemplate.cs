using System.Drawing.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//Added to every move button.
public class MovesetTemplate : MonoBehaviour
{
    TurnManager tracker;
    public GameObject battler;
    public BoxCollider hurtbox;
    public SkillSO moveData;

    [SerializeField] TextMeshProUGUI current;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tracker = TurnManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveData == null)
        {
            return;
        }
    }

    public void AddRange()
    {
        SphereCollider range = battler.GetComponent<SphereCollider>();
        range.radius = moveData.rangeRadius;
    }

    public void ChangeCurrentMoveDisplay()
    {
        current.text = moveData.moveName;
    }

    public void UpdateSelectedMove()
    {
        CharacterClass charClass = battler.GetComponent<CharacterClass>();

        if (charClass == null)
        {
            Debug.Log("No character class found");
            return;
        }

        tracker.attacking = true;
        charClass.currentMove = moveData;
        tracker.currentSkill = moveData;
        tracker.currentPlayer = charClass;
    }
}
