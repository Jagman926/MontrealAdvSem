using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    [SerializeField]
    private GameObject movableObject;
    [SerializeField]
    private Color offColor;
    [SerializeField]
    private Color onColor;
    private SpriteRenderer spriteRenderer;
    //Collider list
    private List<Collider2D> colliderList;

    void Start()
    {
        colliderList = new List<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckIfBlocksDestroyed();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" || col.tag == "Dormant")
        {
			//Add object that entered trigger
            colliderList.Add(col);
            spriteRenderer.color = onColor;
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
                spriteRenderer.color = offColor;
                movableObject.GetComponent<MovableScript>().resetMove = true;
            }
        }
    }

    void CheckIfBlocksDestroyed()
    {
        for(int i = 0; i < colliderList.Count; i++)
        {
            if(colliderList[i] == null)
            {
                colliderList.Remove(colliderList[i]);
            }
        }
    }
}
