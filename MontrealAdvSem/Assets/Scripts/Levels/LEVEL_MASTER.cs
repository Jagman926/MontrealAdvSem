using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LEVEL_MASTER", menuName = "Level/LEVEL_MASTER", order = 1)]
public class LEVEL_MASTER : ScriptableObject 
{
	public string objectName = "LEVEL_MASTER";
	[Header("Level")]
	public Level_Data currentLevel;
	[Header("Level Data")]
	public List<Level_Data> level_Datas;
}
