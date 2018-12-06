using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level/NewLevel", order = 2)]
public class Level_Data : ScriptableObject 
{
	public string objectName = "New Level";
	public string sceneName = "Level_New";
	public List<BlockType.BlockTypes> levelBlockOrder;
	public List<string> lastLevelHighscores_Initials;
	public List<float> lastLevelHighscores_values; 
	public List<string> totalLevelHighscores_Initials;
	public List<float> totalLevelHighscores_values; 
}
