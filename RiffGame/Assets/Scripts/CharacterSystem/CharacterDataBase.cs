using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataBase", menuName = "Scriptable Objects/Character DataBase")]
public class CharacterDataBase : ScriptableObject
{
    public string baseName;
    public List<CharacterData> database;
}
