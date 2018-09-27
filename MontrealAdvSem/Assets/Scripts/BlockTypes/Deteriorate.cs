using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deteriorate : MonoBehaviour {

public bool isDormant;

    private bool isUpdated;

    [Header("Physics Components")]
    public PhysicsMaterial2D physicsMaterial;
    public float mass;
    public int layerNumber;
    private Rigidbody2D rb;

    [Header("Visual Components")]
    public Color color;

	[Header("Deteriorate Variables")]
	public int hitsToBreak;
	public int currentHits;
	public Color break1;
	public Color break2;

	void Start()
    {
        //Set not dormant
        isDormant = false;
        //Get Rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        if(!isDormant)
            CheckDormant();
    }

    void CheckDormant()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color == Color.white)
            UpdateToDormant();
    }

    void UpdateToDormant()
    {
        //Act as ground (For jumping on)
        gameObject.layer = layerNumber;
        //Change mass
        rb.mass = mass;
        //Unfreeze rotation
        rb.constraints = RigidbodyConstraints2D.None;
        //Change color
        gameObject.GetComponent<SpriteRenderer>().color = color;
        //Change physics material
        gameObject.GetComponent<Collider2D>().sharedMaterial = physicsMaterial;
		//Change tag
		gameObject.tag = "Dormant";
        //Change to updated
        isDormant = true;
    }

	    private void OnCollisionEnter2D(Collision2D col)
    {
        if(isDormant && col.transform.tag == "Player")
		{
			//Increment hit
			currentHits++;
			//If hit once
			if(currentHits == 1)
			{
				gameObject.GetComponent<SpriteRenderer>().color = break1;
			}
			//twice
			else if(currentHits == 2)
			{
				gameObject.GetComponent<SpriteRenderer>().color = break2;
			}
			//thrice
			else if(currentHits == 3)
			{
				Destroy(gameObject);
			}
		}
    }
}
