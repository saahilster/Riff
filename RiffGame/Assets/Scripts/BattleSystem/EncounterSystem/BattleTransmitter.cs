using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTransmitter : MonoBehaviour
{
    [SerializeField] BattleData data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransmitData()
    {
        string json = BattleSerializer.SerializeData(data);
        Payload.Set(data.keyName, json);
        SceneManager.LoadScene(1);
    }
}
