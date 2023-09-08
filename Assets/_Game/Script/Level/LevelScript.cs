using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelScript : ScriptableObject
{
    public Level[] levels;
    public string modeName;
}
[System.Serializable]
public class LevelData
{
    public Level level;
}

