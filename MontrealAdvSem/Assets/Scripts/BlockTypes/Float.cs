using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

    public bool isDormant;
    private bool isUpdated;

    [Header("Physics Components")]
    public PhysicsMaterial2D physicsMaterial;
    public float mass;
    public int layerNumber;
    private Rigidbody2D rb;

    [Header("Visual Components")]
    public Color color;

    void Start()
    {
        //Set not dormant
        isDormant = false;
        //Get Rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Check if block has been updated to dormant
        if (!isDormant)
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
		//Freeze Block
		FreezeBlock();
		//Change tag
		gameObject.tag = "Dormant";
        //Change to updated
        isDormant = true;
    }

	void FreezeBlock()
	{
		//Freeze block position
		gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;	
	}
}
