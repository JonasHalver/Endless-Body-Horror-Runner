﻿using System.Collections;
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
    private bool isAirborne = false, onMovingPlatform, isJumping;
    private GameObject platform;

    private Vector2 upGravity = new Vector2 (0, 9.81f);
    private Vector2 downGravity = new Vector2 (0, -9.81f);
    private bool gravityReverse = false;
    private bool recentFlip = false;

    private Animator anim;
    private SpriteRenderer sRenderer;
    public float shoutThreshold = 0.8f;
    private bool checkVolume = true;

    private GameObject respawn;
    private bool isDead;


    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        negativeJump = jumpForce * -1;
        col = gameObject.GetComponent<Collider2D>();
        msHolder = movementSpeed;
        anim = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        respawn = GameObject.FindGameObjectWithTag("Respawn");
	}

    private void Update()
        {
        if (checkVolume)
            {
            if (MicrophoneScript.volume >= shoutThreshold)
                {
                StartCoroutine(Shout());
                }
            else
                {
                anim.SetBool("isShouting", false);
                }
            }

        if (isDead)
            {
            if (Input.GetButtonDown("Reset"))
                {
                transform.position = respawn.transform.position;
                isDead = false;
                anim.SetTrigger("reset");
                }
            }

        if (!onMovingPlatform)
            {
            if (rb.velocity.y != 0)
                {
                isAirborne = true;
                }
            else
                {
                isAirborne = false;
                }
            }
        else
            {
            isAirborne = false;
            }

        if (!isDead)
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
                        isJumping = true;
                        StartCoroutine(JumpDelay());
                        //isAirborne = true;
                        //Jump();
                        anim.SetTrigger("jumpTrigger");
                        }
                    }

                if (Input.GetAxis("Horizontal") == 0)
                    {
                    movementSpeed = msHolder / 2;
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", false);
                    }

                if (Input.GetAxis("Horizontal") != 0)
                    {
                    if (movementSpeed < msHolder)
                        {
                        anim.SetBool("isWalking", true);
                        }
                    if (movementSpeed >= msHolder - 1)
                        {
                        anim.SetBool("isRunning", true);
                        }

                    if (Input.GetAxis("Horizontal") > 0)
                        {
                        sRenderer.flipX = false;
                        }
                    if (Input.GetAxis("Horizontal") < 0)
                        {
                        sRenderer.flipX = true;
                        }
                    }

                if (isAirborne)
                    {
                    movementSpeed = Mathf.Lerp(movementSpeed, msHolder / airMovementMod, 1 * Time.deltaTime);
                    }
                if (!isAirborne)
                    {
                    movementSpeed = Mathf.Lerp(movementSpeed, msHolder, 4 * Time.deltaTime);
                    }
                }
            }
        }

    void FixedUpdate()
        {

        if (!onMovingPlatform)
            {
            anim.SetFloat("upwardsVelocity", rb.velocity.y);
            }
        else
            {
            anim.SetFloat("upwardsVelocity", 0f);
            if (!isJumping)
                {
                rb.velocity = new Vector2(rb.velocity.x, platform.GetComponent<Rigidbody2D>().velocity.y);
                }
            }
            

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
        if (isAirborne)
            {
            if (collision.gameObject.tag == "Ground")
                {
                //isAirborne = false;
                movementSpeed = msHolder / 2;
                recentFlip = false;
                anim.SetTrigger("landedTrigger");
                }
            }

        if (collision.gameObject.tag == "MovingPlatform")
            {
            transform.parent = collision.transform;
            //isAirborne = false;
            onMovingPlatform = true;
            platform = collision.gameObject;
            movementSpeed = msHolder / 2;
            recentFlip = false;
            anim.SetTrigger("landedTrigger");
            }
        }
    private void OnCollisionExit2D(Collision2D collision)
        {
        if (collision.gameObject.tag == "Ground")
            {
            //movementSpeed = msHolder / airMovementMod;
            //isAirborne = true;
            }
        if (collision.gameObject.tag == "MovingPlatform")
            {
            transform.parent = null;
            onMovingPlatform = false;
            platform = null;
            //isAirborne = true;
            }

        }

    IEnumerator JumpDelay()
        {
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
        }

    IEnumerator Shout()
        {
        anim.SetBool("isShouting", true);
        checkVolume = false;
        yield return new WaitForSeconds(1f);
        checkVolume = true;
        }

    private void OnTriggerEnter2D(Collider2D other)
        {
        if (other.tag == "KillZone")
            {
            isDead = true;
            anim.SetTrigger("death");
            }
        if (other.tag == "CamZone")
            {
            if (!onMovingPlatform)
                {
                MicrophoneScript.command = "Stop";
                }
            }
        }
    }
