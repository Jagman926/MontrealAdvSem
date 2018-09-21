using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dormant : MonoBehaviour {

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
        //Set update to false
        isUpdated = false;
        //Get Rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        if(!isUpdated)
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
        //Change to updated
        isUpdated = true;
    }
}
