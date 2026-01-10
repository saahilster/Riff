using System.Configuration;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BattleReciever : MonoBehaviour
{
    public UnityEvent startEncounter;
    public BattleData recievedData;
    private string key = "Battle";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ValidateData();

        if (recievedData == null) Debug.Log("Null data"); 

        Debug.Log(recievedData.arenaID);

        Invoke("StartEvent", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ValidateData()
    {
        recievedData = EncounterCollector.Consume(key);
    }

    private void StartEvent()
    {
        startEncounter.Invoke();
    }
}
