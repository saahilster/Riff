using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RegisterParty : MonoBehaviour
{
    [SerializeField] CharacterDataBase characterBase;
    public Dictionary<int, CharacterData> registry = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AssignValue();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignValue()
    {
        for (int i = 0; i < characterBase.database.Count; i++)
        {
            registry[i] = characterBase.database[i];
            Debug.Log($"Added {characterBase.database[i].keyName} character at position {i}");
        }
    }
}
