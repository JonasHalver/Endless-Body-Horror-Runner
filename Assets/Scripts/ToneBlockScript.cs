using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToneBlockScript : MonoBehaviour
    {

    public float soundThreshold = 0.99f;
    private bool withinEarshot;
    private BoxCollider2D col;
    public Collider2D earshotCol;
    public Collider2D listenerCol;

    public bool resize;
    public float resizeSpeed = 1;

    private Vector3 startScale;
    public Vector3 targetScale;

    public bool move;
    private Vector3 startPos;
    public Vector3 targetPos;
    public float moveSpeed = 1;

    private SpriteRenderer sRenderer;

    // Use this for initialization
    void Start()
        {
        startPos = transform.position;
        startScale = transform.localScale;
        col = gameObject.GetComponent<BoxCollider2D>();
        earshotCol = GameObject.Find("Earshot").GetComponent<Collider2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        }

    // Update is called once per frame
    void Update()
        {
        if (listenerCol.IsTouching(earshotCol))
            {
            withinEarshot = true;
            }
        else if (!listenerCol.IsTouching(earshotCol))
            {
            withinEarshot = false;
            }

        if (withinEarshot)
            {
            Debug.Log(gameObject.name + "is within earshot.");
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
        //transform.localScale = Vector3.Lerp(transform.localScale, targetScale, resizeSpeed * Time.deltaTime);
        sRenderer.size = new Vector2(Mathf.Lerp(sRenderer.size.x, targetScale.x, resizeSpeed * Time.deltaTime), sRenderer.size.y);
        col.size = new Vector2(Mathf.Lerp(col.size.x, targetScale.x - 3, resizeSpeed * Time.deltaTime), col.size.y);
        }

    public void SizeReset()
        {
        //transform.localScale = Vector3.Lerp(transform.localScale, startScale, resizeSpeed * Time.deltaTime);
        sRenderer.size = new Vector2(Mathf.Lerp(sRenderer.size.x, 4, resizeSpeed * Time.deltaTime), sRenderer.size.y);
        col.size = new Vector2(Mathf.Lerp(col.size.x, 1, resizeSpeed * Time.deltaTime), col.size.y);
        }

    public void Move()
        {
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

    public void PositionReset()
        {
        transform.position = Vector3.Lerp(transform.position, startPos, moveSpeed * Time.deltaTime);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //    {
    //    if (collision.gameObject.name == "Earshot")
    //        {
    //        withinEarshot = true;
    //        }
    //    }
    //
    //private void OnCollisionExit2D(Collision2D collision)
    //    {
    //    if (collision.gameObject.name == "Earshot")
    //        {
    //        withinEarshot = false;
    //        }
    //    }
    //}
