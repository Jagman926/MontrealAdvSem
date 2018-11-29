using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    [SerializeField]
    private GameObject movableObject;

    private List<Collider2D> colliderList;

    void Start()
    {
        colliderList = new List<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" || col.tag == "Dormant")
        {
			//Add object that entered trigger
            colliderList.Add(col);
            movableObject.GetComponent<MovableScript>().move = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" || col.tag == "Dormant")
        {
			//Remove object that left collider
            if (colliderList.Contains(col))
            {
                colliderList.Remove(col);
            }
			//If Empty reset move
            if (colliderList.Count == 0)
            {
                movableObject.GetComponent<MovableScript>().resetMove = true;
            }
        }
    }
}
