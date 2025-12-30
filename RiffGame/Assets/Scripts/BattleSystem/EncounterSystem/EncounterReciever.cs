using UnityEngine;
using UnityEngine.Events;

public class EncounterReciever : MonoBehaviour
{
    public Event startLoad;
    public string key = "Battle";
    public BattleData data;
    public string _arenaID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Attempting to retrieve battle data");
        Debug.Log(Payload.Has(key));
        Debug.Log("Key retrieved. Booting Encounter Up..");
        string json = Payload.Consume(key);
        data = BattleSerializer.DeserializeData(json);
        Debug.Log(data.arenaID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
