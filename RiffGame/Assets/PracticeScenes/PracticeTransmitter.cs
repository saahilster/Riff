using UnityEngine;
using UnityEngine.SceneManagement;

public class PracticeTransmitter : MonoBehaviour
{

    public void Start()
    {
        Debug.Log("Initialized sending data in 2 seconds");
        Invoke("TransmitData", 2 );
    }

    private PracticeData BuildPackage()
    {
        return new PracticeData
        {
            enemyPartyID = new[] {"slime", "slime", "boar"},
            playerPartyID = new[] {"Mage", "Medic", "Warrior"},
            arenaID = "starter"
        };
    }
    public void TransmitData()
    {
        PracticeData data = BuildPackage();

        string json = PracticeSerializer.SerializeData(data);

        PracticePackager.Set("pending data", json);
        SceneManager.LoadScene(1);
    }
}
