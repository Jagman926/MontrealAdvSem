using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour {

    public bool isDormant;
    private bool isUpdated;

    [Header("Physics Components")]
    public PhysicsMaterial2D physicsMaterial;
    public float mass;
    public int layerNumber;
    private Rigidbody2D rb;

    [Header("Visual Components")]
    public Color color;
	public Color swapColor;

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
		{
            CheckDormant();

			//Check if action button pressed
			if(Managers.InputManager.Instance.playerAction)
			{
				CheckSwap();
			}
		}
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
		//Set all other swap targets to default
		SetSwapTarget();
        //Change color
        gameObject.GetComponent<SpriteRenderer>().color = swapColor;
        //Change physics material
        gameObject.GetComponent<Collider2D>().sharedMaterial = physicsMaterial;
        //Change to updated
        isDormant = true;
    }

	void CheckSwap()
	{
		if(GameObject.FindGameObjectWithTag("SwapTarget"))
		{
			//Get target
			GameObject target = GameObject.FindGameObjectWithTag("SwapTarget");
			//Swap positions
			swapPositions(gameObject, target);
			//Set targets tag and color to default
			target.tag = "Dormant";
			target.GetComponent<SpriteRenderer>().color = color;
		}		
	}

	void swapPositions(GameObject object1, GameObject object2)
	{
 		Vector3 tempPosition = object1.transform.position;
		Vector3 tempVelocity = object1.GetComponent<Rigidbody2D>().velocity;

		//Swap positions
 		object1.transform.position = object2.transform.position;
 		object2.transform.position = tempPosition;
	}

	void SetSwapTarget()
	{
		//Get all gameobjects with tag
		GameObject[] objects = GameObject.FindGameObjectsWithTag("SwapTarget");
		//Set them all to default
		foreach (GameObject obj in objects)
		{
			obj.tag = "Dormant";
			obj.GetComponent<SpriteRenderer>().color = color;
		}
		//Change current tag to target
		gameObject.tag = "SwapTarget";
	}
}
