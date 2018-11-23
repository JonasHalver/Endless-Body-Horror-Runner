using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {

    private Rigidbody2D rb;
    public float movementSpeed = 10;
    public float maxSpeed = 5;
    public float jumpForce = 5f;
    private float negativeJump;
    private float jump;

    private Collider2D col;
    public bool isAirborne = false;

    private Vector2 upGravity = new Vector2 (0, 9.81f);
    private Vector2 downGravity = new Vector2 (0, -9.81f);
    private bool gravityReverse = false;
    private bool recentFlip = false;

    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        negativeJump = jumpForce * -1;
        col = gameObject.GetComponent<Collider2D>();
	}


    void FixedUpdate()
        {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), 0f) * movementSpeed);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        
        if (Input.GetButtonDown("Jump"))
            {
            if (!isAirborne)
                {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                //isAirborne = true;
                }
            }
          //Gravity Flipper 
        //if (!recentFlip)
        //    {
        //    if (Input.GetKeyDown("up"))
        //        {
        //        gravityReverse = true;
        //        recentFlip = true;
        //        }
        //
        //    if (Input.GetKeyDown("down"))
        //        {
        //        gravityReverse = false;
        //        recentFlip = true;
        //        }
        //    }
        //
        //if (gravityReverse)
        //    {
        //    jump = negativeJump;
        //    Physics2D.gravity = upGravity;
        //    }
        //if (!gravityReverse)
        //    {
        //    jump = jumpForce;
        //    Physics2D.gravity = downGravity;
        //    }
        }

    private void OnCollisionEnter2D(Collision2D collision)
        {
        if (collision.gameObject.tag == "Ground")
            {
            isAirborne = false;
            recentFlip = false;
            }
        }
    private void OnCollisionExit2D(Collision2D collision)
        {
        if (collision.gameObject.tag == "Ground")
            {
            isAirborne = true;
            }
        }
    }
