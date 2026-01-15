using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.Events;

//Turn manager is the validator for all actions being taken place.
//need to create an event where the move data is stored in here and then acts upon those per turn 
//Also need to delete data that has expired turns
public class TurnManager : MonoBehaviour
{
    public BattleTracker tracker;
    public UnityEvent PlayerTurnEvent;
    public UnityEvent EnemyTurnEvent;
    public List<CharacterClass> party;
    public List<CharacterClass> opps;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        LifeCycle();
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
        //W/L Conditions
        if (party == null)
        {
            Debug.Log("Lost");
            return;
        }
        else if (opps == null)
        {
            Debug.Log("Won match");
            return;
        }

        PlayerTurnEvent.Invoke();
        EnemyTurnEvent.Invoke();
    }
}
