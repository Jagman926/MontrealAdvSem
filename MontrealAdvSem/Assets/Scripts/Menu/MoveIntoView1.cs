using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveIntoView1 : MonoBehaviour {

	void Start () 
	{
		transform.DOLocalMoveY(5.0f,Random.Range(0.5f, 1.5f)).From();
	}
}
