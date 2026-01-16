using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

//Turn manager is the validator for all actions being taken place.
//need to create an event where the move data is stored in here and then acts upon those per turn 
//Also need to delete data that has expired turns
public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public SkillSO currentSkill;
    public CharacterClass currentPlayer;
    public bool attacking;

    public List<CharacterClass> party;
    public List<CharacterClass> opps;
    [SerializeField] GameObject attackingMode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        currentPlayer = null;
        currentSkill = null;

        party ??= new List<CharacterClass>();
        opps ??= new List<CharacterClass>();
    }
    void Start()
    {
        attacking = false;
    }
    // Update is called once per frame
    void Update()
    {
        LifeCycle();
        UpdateAttackDisplay();
    }

    /// <summary>
    /// This function is supposed to be used by SpawnBattleParty to add characterclass to party/opps
    /// </summary>
    /// <param name="character">Pass the CharacterClass after a population.</param>
    /// <param name="enemy">If true this will be added to the enemy party.</param>
    public void AddCharacter(CharacterClass character, bool enemy)
    {
        if (enemy)
        {
            opps.Add(character);
        }
        else
        {
            party.Add(character);
        }
    }

    public void LifeCycle()
    {
        if (party == null || opps == null)
        {
            Debug.LogError("Party lists not initialized!");
            return;
        }

        party.RemoveAll(x => x == null);
        opps.RemoveAll(x => x == null);

        //W/L Conditions
        if (party.Count == 0)
        {
            Debug.Log("Lost");
        }
        else if (opps.Count == 0)
        {
            Debug.Log("Won match");
        }
    }

    public void UpdateAttackDisplay()
    {
        attackingMode.SetActive(attacking);
    }
}
