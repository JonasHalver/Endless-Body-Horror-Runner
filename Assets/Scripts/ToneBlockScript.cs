using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToneBlockScript : MonoBehaviour {

    public float soundThreshold = 0.99f;
    private bool withinEarshot;
    private Collider2D col;
    public Collider2D earshotCol;

    public bool resize;
    public float resizeSpeed = 1;

    private Vector3 startScale;
    public Vector3 targetScale;

    public bool move;
    private Vector3 startPos;
    public Vector3 targetPos;
    public float moveSpeed = 1;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        startScale = transform.localScale;
        col = gameObject.GetComponent<Collider2D>();
        earshotCol = GameObject.Find("Earshot").GetComponent<Collider2D>();
        }
	
	// Update is called once per frame
	void Update () {
        if (col.IsTouching(earshotCol))
            {
            withinEarshot = true;
            }
        else if (!col.IsTouching(earshotCol))
            {
            withinEarshot = false;
            }

        if (withinEarshot)
            {
            if (MicrophoneScript.volume >= soundThreshold)
                {
                if (resize)
                    {
                    Resize();
                    }
                if (move)
                    {
                    Move();
                    }
                }
            else
                {
                if (resize)
                    {
                    SizeReset();
                    }
                if (move)
                    {
                    PositionReset();
                    }
                }
            }
	}

    public void Resize()
        {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, resizeSpeed * Time.deltaTime);
        }

    public void SizeReset()
        {
        transform.localScale = Vector3.Lerp(transform.localScale, startScale, resizeSpeed * Time.deltaTime);
        }

    public void Move()
        {
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

    public void PositionReset()
        {
        transform.position = Vector3.Lerp(transform.position, startPos, moveSpeed * Time.deltaTime);
        }

    private void OnCollisionEnter2D(Collision2D collision)
        {
        if (collision.gameObject.name == "Earshot")
            {
            withinEarshot = true;
            }
        }

    private void OnCollisionExit2D(Collision2D collision)
        {
        if (collision.gameObject.name == "Earshot")
            {
            withinEarshot = false;
            }
        }
    }
