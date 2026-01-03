using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovesetDatabaseSO", menuName = "Scriptable Objects/MovesetDatabaseSO")]
public class MovesetDatabaseSO : ScriptableObject
{
    public List<SkillSO> moveDatas;
}
