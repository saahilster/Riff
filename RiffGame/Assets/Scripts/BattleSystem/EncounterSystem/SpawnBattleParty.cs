using Unity.VisualScripting;
using UnityEngine;

public class SpawnBattleParty : MonoBehaviour
{
    [SerializeField] CharacterDataBase party;
    [SerializeField] Transform spot1;
    [SerializeField] Transform spot2;
    [SerializeField] Transform spot3;

    private Transform[] partyPositions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        partyPositions = new Transform[] {spot1, spot2, spot3};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnMembers()
    {
        for (int i = 0; i < party.database.Count; i++)
        {
            Instantiate(party.database[i].battlePrefab, partyPositions[i].position, partyPositions[i].rotation);
            Debug.Log($"Spawned {party.database[i].displayName} at {partyPositions[i].position}");
        }
    }
}
