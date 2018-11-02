using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {

    public bool isDormant;
    private bool isUpdated;

    [Header("Physics Components")]
    public PhysicsMaterial2D physicsMaterial;
    public float mass;
    public int layerNumber;
    private Rigidbody2D rb;

    [Header("Sprite Components")]
    public Sprite sprite;

	[Header("Melt Variables")]
	public int timeToMelt;
	private float meltingTimer;

	void Start()
    {
        //Set not dormant
        isDormant = false;
        //Get Rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
		//Set meting timer
		meltingTimer = 0.0f;
	}

    void Update()
    {
        if(!isDormant)
            CheckDormant();
		else
			MeltBlock();
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

	void MeltBlock()
	{
		//If it's still not fully melted
		if(transform.localScale.x > 1.0f/timeToMelt)
		{
			//Increment timer
			meltingTimer += Time.deltaTime;
			//If it's beenna second
			if(meltingTimer > 1.0f)
			{
				//Shrink block
				transform.localScale -= new Vector3(1.0f/timeToMelt, 1.0f/timeToMelt, 0);
				//Reset timer
				meltingTimer = 0.0f;
			}
		}
	}
}
