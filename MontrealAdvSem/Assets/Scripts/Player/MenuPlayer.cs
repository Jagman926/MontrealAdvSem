using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    //Current Player
    private Rigidbody2D currPlayerRB;

    [Header("Movement Values")]
    public float moveSpeed = 0;
    public float jumpPower = 0;
    public float gravityPower = 0;
    float targetMoveSpeed = 0;

    //IsGrounded
    [Header("Grounded Settings")]
    public bool isGrounded;
    public LayerMask groundLayers;

    void Start()
    {
        currPlayerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Update player movemen
        UpdateMovement();
        UpdateJump();
        if (currPlayerRB.IsSleeping())
            currPlayerRB.WakeUp();
    }

    void UpdateMovement()
    {
        //Player Movement
        targetMoveSpeed = Mathf.Lerp(currPlayerRB.velocity.x, Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Time.deltaTime * 10);
        currPlayerRB.velocity = new Vector2(targetMoveSpeed, currPlayerRB.velocity.y);
    }

    void UpdateJump()
    {
        //Check Grounding
        CheckGrounding();

        //Player Jump
        if (Managers.InputManager.Instance.playerJump && isGrounded)
        {
            currPlayerRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            //Play jump sound
            EventParam eventParam = new EventParam();
            Managers.EventManager.TriggerEvent("PlayJumpSound", eventParam);
        }
        //Downward Force
        if (!isGrounded)
        {
            currPlayerRB.AddForce(Vector2.down * gravityPower, ForceMode2D.Impulse);
        }
    }

    void CheckGrounding()
    {
        //Check overlap to see if on groundLayer
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - .45f, transform.position.y - .5f),
            new Vector2(transform.position.x + .45f, transform.position.y - .505f), groundLayers);
    }
}
