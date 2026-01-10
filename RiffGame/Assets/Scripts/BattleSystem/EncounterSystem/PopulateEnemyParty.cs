using Unity.VisualScripting;
using UnityEngine;

public class PopulateEnemyParty : MonoBehaviour
{
    //Reference to battle reciever to collect battle data from enemies to populate enemy side.
    [SerializeField] private BattleReciever BR;
    //use this for enemy
    [SerializeField] CharacterDataBase enemyParty;
    private BattleData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (BR == null)
        {
            return;
        }

        data = BR.recievedData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateOpps()
    {
        CharacterData[] enemies = BR.recievedData.enemyParty;

        if(enemies == null)
        {
            Debug.LogWarning("Enemy data null check");
            return;
        }

        foreach(CharacterData enemy in enemies)
        {
            Debug.Log(enemy.keyName);
            enemyParty.database.Add(enemy);
        }
    }
}
