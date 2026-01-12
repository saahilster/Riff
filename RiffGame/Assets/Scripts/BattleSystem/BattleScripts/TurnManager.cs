using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Turn manager is the validator for all actions being taken place.
//need to create an event where the move data is stored in here and then acts upon those per turn 
//Also need to delete data that has expired turns
public class TurnManager : MonoBehaviour
{
    public UnityEvent PlayerTurn;
    public UnityEvent EnemyTurn;
    public int currentTurn;
    public int totalTurn = 0;
    public List<CharacterClass> party;
    public List<CharacterClass> opps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerTurn.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
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

}
