using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Current Player
    public GameObject currPlayer;
    public Rigidbody2D currPlayerRB;

    [Header("Movement Values")]
    public float moveSpeed = 0;
    public float jumpPower = 0;
    public float gravityPower = 0;
    float targetMoveSpeed = 0;

    //IsGrounded
    [Header("Grounded Settings")]
    public bool isGrounded;
    public LayerMask groundLayers;

    void Update()
    {
        if(Managers.PlayerManager.Instance.currPlayer != null && Time.timeScale != 0)
        {
            //Update player movement
            UpdateCurrentPlayer();
            UpdateMovement();
            UpdateJump();
        }
    }

    void UpdateCurrentPlayer()
    {
        currPlayer = Managers.PlayerManager.Instance.currPlayer;
        currPlayerRB = currPlayer.GetComponent<Rigidbody2D>();
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
        isGrounded = Physics2D.OverlapArea(new Vector2(currPlayer.transform.position.x - .48f, currPlayer.transform.position.y - .5f),
            new Vector2(currPlayer.transform.position.x + .48f, currPlayer.transform.position.y - .505f), groundLayers);
    }

    void OnDrawGizmos()
    {
        if (currPlayer != null)
        {
            //Draw overlap
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(new Vector2(currPlayer.transform.position.x, currPlayer.transform.position.y - .505f), new Vector2(0.96f, 0.01f));
        }
    }
}
