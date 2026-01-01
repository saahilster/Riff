using UnityEngine;

public class PracticeReciever : MonoBehaviour
{
    string key = "pending data";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PracticePackager.Has(key))
        {
            Debug.Log("Key Found!");
            
        }

        UnpackageData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnpackageData()
    {
        if (!PracticePackager.Has(key))
        {
            Debug.Log("No key found");
            return;
        }

        string json = PracticePackager.Consume(key);

        PracticeData data = PracticeSerializer.DeserializeData(json);
        
        Debug.Log(data.arenaID);

        foreach (string players in data.playerPartyID){
            Debug.Log(players);
        }

        foreach (string opps in data.enemyPartyID)
        {
            Debug.Log(opps);
        }
    }
}
