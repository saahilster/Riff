using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] BattleReciever encounter;
    [SerializeField] List<ArenaData> arenaRegistry;
    private string arenaKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (encounter == null) Debug.Log("Encounter invalid");

        arenaKey = encounter.recievedData.arenaID;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnArena()
    {
        foreach (ArenaData data in arenaRegistry)
        {
            if (arenaKey == data.arenaName)
            {
                Debug.Log($"found {data.arenaName}");
                Instantiate(data.arenaPrefab, new Vector3(0,0,0), new Quaternion(0,0,0,0));
                Debug.Log("Spawned floor");
                break;           
            }
            else
            {
                Debug.Log($"not found {data.arenaName}");
            }
        }
    }
}
