using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO")]
public class LevelDataManager : ScriptableObject
{
    [SerializeField] private List<LevelData> levels;

    public LevelData GetLevelData(int levelIndex)
    {
        return levels[levelIndex];
    }
}
