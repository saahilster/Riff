using System;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] EncounterReciever encounter;
    [SerializeField] public List<ArenaData> arenas;
    string arenaData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (encounter == null)
        {
            Debug.LogError("Cannot find encounter");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnArena()
    {
        arenaData = encounter._arenaID;
        //need to iterate through arena names to see if the arena data fits
        foreach(var arena in arenas)
        {
            Debug.Log(arena);
            if(arena.arenaName == arenaData)
            {
                Instantiate(arena.prefab, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            }
        }
    }
}
