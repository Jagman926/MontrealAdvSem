using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Components
    Rigidbody2D rb;
    //Movement Variables
    public bool movable;
    [Header("Movement Values")]
    public float moveSpeed = 0;
    public float jumpPower = 0;
    public float gravityPower = 0;
    float targetMoveSpeed = 0;
    //IsGrounded
    [Header("Grounded Settings")]
    public bool isGrounded;
    public LayerMask groundLayers;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        movable = true;
	}

    void Update ()
    {
        if (movable)
        {
            UpdateMovement();
            UpdateJump();
        }
	}

    void UpdateMovement()
    {
        //Player Movement
        targetMoveSpeed = Mathf.Lerp(rb.velocity.x, Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Time.deltaTime * 10);
        rb.velocity = new Vector2(targetMoveSpeed, rb.velocity.y);

        //Player Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void UpdateJump()
    {
        //Check Grounding
        CheckGrounding();

        //Player Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        //Downward Force
        if (!isGrounded)
        {
            rb.AddForce(Vector2.down * gravityPower, ForceMode2D.Impulse);
        }
    }

    void CheckGrounding()
    {
        //Check overlap to see if on groundLayer
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - .48f, transform.position.y - .5f),
            new Vector2(transform.position.x + .48f, transform.position.y - .505f), groundLayers);
    }

    void OnDrawGizmos()
    {
        //Draw overlap
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - .505f), 
            new Vector2(0.96f, 0.01f));
    }
}
