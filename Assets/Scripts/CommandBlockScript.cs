using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBlockScript : MonoBehaviour {

    //public Vector3 leftPos;
    //public Vector3 rightPos;

    //public float offset;
    public float moveSpeed = 20f;

    private Vector3 left = new Vector3(-1, 0), right = new Vector3(1, 0), up = new Vector3(0, 1), down = new Vector3(0, -1);
    public enum Directions { Up, Down, Left, Right, Stop };
    public Directions dir = Directions.Stop;
    private Rigidbody2D rb;
    private string lastDir;
    //public bool startLeft;
    //public bool startRight;
    //private Vector3 pos;
    //
    //private bool left;
    //private bool right;
    //
    //private bool withinEarshot;
    //private Collider2D col;
    //public Collider2D earshotCol;
    //public Collider2D listenerCol;
    //
    //private SpriteRenderer sRenderer;
    //public Sprite leftSprite;
    //public Sprite rightSprite;

    void Start () {
        gameObject.tag = "MovingPlatform";
        rb = GetComponent<Rigidbody2D>();
        //col = gameObject.GetComponent<Collider2D>();
        //earshotCol = GameObject.Find("Earshot").GetComponent<Collider2D>();
        //pos = transform.position;
        //sRenderer = GetComponent<SpriteRenderer>();
        //if (startLeft)
        //    {
        //    sRenderer.sprite = rightSprite;
        //    }
        //else
        //    {
        //    sRenderer.sprite = leftSprite;
        //    }
	}

    // Update is called once per frame
    void Update()
        {
        switch (dir)
            {
            case Directions.Up:
                //transform.position = transform.position + (up * moveSpeed * Time.deltaTime);
                if (lastDir != "Up")
                    {
                    lastDir = null;
                    rb.velocity = up * moveSpeed * Time.deltaTime;
                    }
                break;
            case Directions.Down:
                //transform.position = transform.position + (down * moveSpeed * Time.deltaTime);
                if (lastDir != "Down")
                    {
                    lastDir = null;
                    rb.velocity = down * moveSpeed * Time.deltaTime;
                    }
                break;
            case Directions.Left:
                //transform.position = transform.position + (left * moveSpeed * Time.deltaTime);
                if (lastDir != "Left")
                    {
                    lastDir = null;
                    rb.velocity = left * moveSpeed * Time.deltaTime;
                    }
                break;
            case Directions.Right:
                //transform.position = transform.position + (right * moveSpeed * Time.deltaTime);
                if (lastDir != "Right")
                    {
                    lastDir = null;
                    rb.velocity = right * moveSpeed * Time.deltaTime;
                    }
                break;
            case Directions.Stop:
                rb.velocity = new Vector2(0,0);
                break;
            }
        //if (MicrophoneScript.command == "Left")
        //    {
        //    left = true;
        //    right = false;
        //    }
        //else if (MicrophoneScript.command == "Right")
        //    {
        //    left = false;
        //    right = true;
        //    }
        //
        //if (listenerCol.IsTouching(earshotCol))
        //    {
        //    withinEarshot = true;
        //    }
        //else if (!listenerCol.IsTouching(earshotCol))
        //    {
        //    withinEarshot = false;
        //    }
        //
        //if (withinEarshot)
        //    {
        //    if (startLeft)
        //        {
        //        Vector3 targetPos = new Vector3(pos.x + offset, pos.y);
        //        if (left)
        //            {
        //            transform.position = Vector3.Lerp(transform.position, pos, moveSpeed * Time.deltaTime);
        //            }
        //        if (right)
        //            {
        //            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        //            }
        //        }
        //
        //    if (startRight)
        //        {
        //        Vector3 targetPos = new Vector3(pos.x - offset, pos.y);
        //
        //        if (left)
        //            {
        //            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        //            }
        //        if (right)
        //            {
        //            transform.position = Vector3.Lerp(transform.position, pos, moveSpeed * Time.deltaTime);
        //            }
        //        }
        //    }
        }

    private void OnCollisionEnter2D(Collision2D collision)
        {
        if (collision.gameObject.tag != "Player")
            {
            switch (dir)
                {
                case Directions.Up:
                    lastDir = "Up";
                    break;
                case Directions.Down:
                    lastDir = "Down";
                    break;
                case Directions.Left:
                    lastDir = "Left";
                    break;
                case Directions.Right:
                    lastDir = "Right";
                    break;
                case Directions.Stop:
                    break;
                }
            MicrophoneScript.command = "Stop";
            }
        }
    }
    

