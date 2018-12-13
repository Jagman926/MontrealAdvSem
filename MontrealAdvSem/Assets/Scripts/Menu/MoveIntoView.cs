using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveIntoView : MonoBehaviour {

	void Start () 
	{
		transform.DOLocalMoveY(8.80f,Random.Range(1.0f, 3.0f)).From();
	}
}
