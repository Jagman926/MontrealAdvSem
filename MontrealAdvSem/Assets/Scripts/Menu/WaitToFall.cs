using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToFall : MonoBehaviour {

	private IEnumerator coroutine;
	[SerializeField]
	private GameObject player;

	void Awake()
	{
		player.SetActive(false);
	}

	void Start () 
	{
		coroutine = WaitAndActivate(1.7f);
        StartCoroutine(coroutine);
	}

    private IEnumerator WaitAndActivate(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            player.SetActive(true);
        }
    }
}
