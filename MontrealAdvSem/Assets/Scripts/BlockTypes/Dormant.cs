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

    [Header("Sprite Components")]
    public Sprite sprite;

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
        //Change sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        //Change physics material
        gameObject.GetComponent<Collider2D>().sharedMaterial = physicsMaterial;
        //Change tag
		gameObject.tag = "Dormant";
        //Change to updated
        isDormant = true;
    }
}
