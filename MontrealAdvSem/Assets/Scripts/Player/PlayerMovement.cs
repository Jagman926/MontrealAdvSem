using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Pause Check
    [HideInInspector]
    public bool isPaused;
    //Current Player
    private GameObject currPlayer;
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
        isPaused = false;
    }

    void Update()
    {
        if(currPlayer != null && !isPaused)
        {
            //Update player movement
            UpdateMovement();
            UpdateJump();
        }
    }

    public void UpdateCurrentPlayer(GameObject player)
    {
        currPlayer = player;
        currPlayerRB = player.GetComponent<Rigidbody2D>();
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
        isGrounded = Physics2D.OverlapArea(new Vector2(currPlayer.transform.position.x - .45f, currPlayer.transform.position.y - .5f),
            new Vector2(currPlayer.transform.position.x + .45f, currPlayer.transform.position.y - .505f), groundLayers);
    }
}
