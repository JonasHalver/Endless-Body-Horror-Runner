using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {

    public bool usePhysics = true;

    private Rigidbody2D rb;
    public float movementSpeed = 10;
    public float maxSpeed = 5;
    public float jumpForce = 5f;
    private float negativeJump;
    private float jump;
    public float airMovementMod = 2f;
    private float msHolder; 

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
        msHolder = movementSpeed;
	}

    private void Update()
        {
        if (!usePhysics)
            {
                  
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.Translate(movement * (movementSpeed * Time.deltaTime));

            if (Input.GetButtonDown("Jump"))
                {
                if (!isAirborne)
                    {
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    //isAirborne = true;
                    //Jump();
                    }
                }

           if (Input.GetAxis("Horizontal") == 0)
                {
                movementSpeed = msHolder / 2;
                }

            if (isAirborne)
                {
                movementSpeed = Mathf.Lerp(movementSpeed, msHolder / airMovementMod, 1 * Time.deltaTime);
                }
            if (!isAirborne)
                {
                movementSpeed = Mathf.Lerp(movementSpeed, msHolder, 1 * Time.deltaTime);
                }
            }
        }

    void FixedUpdate()
        {
        if (usePhysics)
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
            }
        }

    private void OnCollisionEnter2D(Collision2D collision)
        {
        if (collision.gameObject.tag == "Ground")
            {
            isAirborne = false;
            movementSpeed = msHolder / 2;
            recentFlip = false;
            }
        }
    private void OnCollisionExit2D(Collision2D collision)
        {
        if (collision.gameObject.tag == "Ground")
            {
            //movementSpeed = msHolder / airMovementMod;
            isAirborne = true;
            
            }
        }
    }
