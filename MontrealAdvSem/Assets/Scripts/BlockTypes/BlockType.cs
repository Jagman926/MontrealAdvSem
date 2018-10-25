using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType : MonoBehaviour 
{
	public enum BlockTypes
	{
		DORMANT,
		FLOATING,
		STICKY,
		DETERIORATE,
		ICE,
		SWAP
	}

	[SerializeField]
	public List<GameObject> blockTypesList;

	private void Start()
	{
		CheckBlockList();
	}

	private void CheckBlockList()
	{
		if(blockTypesList.Count == 0)
		{
			Debug.Log("BLOCK LIST ERROR: Block List is EMPTY");
		}
	}

	public GameObject ReturnBlockType(BlockTypes blockType)
	{
		switch (blockType)
		{
			//Return Dormant
			case BlockTypes.DORMANT:
			{
				return blockTypesList[0];
			}
			//Return Floating
			case BlockTypes.FLOATING:
			{
				return blockTypesList[1];
			}
			//Return Sticky
			case BlockTypes.STICKY:
			{
				return blockTypesList[2];
			}
			//Return Deteriorate
			case BlockTypes.DETERIORATE:
			{
				return blockTypesList[3];
			}
			//Return Ice
			case BlockTypes.ICE:
			{
				return blockTypesList[4];
			}
			//Return Swap
			case BlockTypes.SWAP:
			{
				return blockTypesList[5];
			}
			//Default
			default:
			{
				Debug.Log("BLOCK RETURN ERROR: Unknown Block");
				return null;
			}
		}
	}
}
