using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptables.LevelManager.SetDifficutly(Levels.LevelList.EASY);
//Scriptables.LevelManager.CompletedLevel();

[CreateAssetMenu(fileName ="level",menuName ="Scriptable Levels")]
public class Levels : ScriptableObject
{
    public float totalLevels, levelsLeft;

    public int columnSize, rowSize; 

}
