using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level", fileName = "new Level")]
public class LevelSettingsSO : ScriptableObject
{
    // Add all the settings for the specific level 
    [SerializeField] private GameConstants.Scenes level;

    public string GetLevelScene()
    {
        return level.ToString();
    }
}
