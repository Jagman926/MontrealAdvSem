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
    private float yHeight;
    private float xHeight;

	[Header("Deteriorate Variables")]
	public int hitsToBreak;
	public int currentHits;

    [Header("Sprite Components")]
    public Sprite noBreak;
	public Sprite break0;
	public Sprite break1;
    public Sprite break2;

    [Header("Particle System")]
    public ParticleSystem destroyPS;

	void Start()
    {
        //Set not dormant
        isDormant = false;
        //Get Rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
        yHeight = gameObject.GetComponent<BoxCollider2D>().size.y -.1f;
        xHeight = gameObject.GetComponent<BoxCollider2D>().size.x;
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
        gameObject.GetComponent<SpriteRenderer>().sprite = noBreak;
        //Change physics material
        gameObject.GetComponent<Collider2D>().sharedMaterial = physicsMaterial;
		//Change tag
		gameObject.tag = "Dormant";
        //Change to updated
        isDormant = true;
    }

	    private void OnCollisionEnter2D(Collision2D col)
    {
        if(isDormant && col.transform.tag == "Player" && 
        (col.transform.position.y > transform.position.y + yHeight && col.transform.position.x > transform.position.x - xHeight))
		{
			//Increment hit
			currentHits++;
			//If hit once
			if(currentHits == 1)
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = break0;
			}
			//twice
			else if(currentHits == 2)
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = break1;
			}
			//thrice
			else if(currentHits == 3)
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = break2;
			}
            //frice
            else if(currentHits == 4)
            {
                Instantiate(destroyPS, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
		}
    }
}
