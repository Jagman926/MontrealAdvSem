using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovableScript : MonoBehaviour 
{
	[Header("Move variables")]
	[SerializeField]
	private float endX;
	[SerializeField]
	public float endY;
	private Vector3 startPostion;
	private Vector3 endPosition;
	[SerializeField]
	public float moveDuration;

	[Header("Bool variables")]
	public bool move;
	public bool resetMove;

	void Start () 
	{
		//Set positions
		startPostion = gameObject.transform.position;
		endPosition = new Vector3(endX, endY, gameObject.transform.position.z);
	}
	
	void Update () 
	{
		CheckMove();
	}

	private void CheckMove()
	{
		if(move)
		{
			MoveObject();
			move = false;
		}
		if(resetMove)
		{
			ResetObject();
			resetMove = false;
		}
	}

	private void MoveObject()
	{
		transform.DOMove(endPosition, moveDuration, false);
	}

	private void ResetObject()
	{
		transform.DOMove(startPostion, moveDuration, false);
	}
}
