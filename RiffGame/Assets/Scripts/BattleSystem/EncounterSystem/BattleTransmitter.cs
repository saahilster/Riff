using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class BattleTransmitter : MonoBehaviour
{
    [SerializeField] BattleData battleData;
    private string key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        key = battleData.keyName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEncounter()
    {
        EncounterCollector.RegisterData(key, battleData);
        SceneManager.LoadScene(1);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartEncounter();
        }
    }
}
