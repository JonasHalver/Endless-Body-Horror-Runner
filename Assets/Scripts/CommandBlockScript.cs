using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBlockScript : MonoBehaviour {

    //public Vector3 leftPos;
    //public Vector3 rightPos;

    public float offset;
    public float moveSpeed = 1f;

    public bool startLeft;
    public bool startRight;
    private Vector3 pos;

    private bool left;
    private bool right;

    private bool withinEarshot;
    private Collider2D col;
    public Collider2D earshotCol;
    public Collider2D listenerCol;

    private SpriteRenderer sRenderer;
    public Sprite leftSprite;
    public Sprite rightSprite;

    void Start () {
        col = gameObject.GetComponent<Collider2D>();
        earshotCol = GameObject.Find("Earshot").GetComponent<Collider2D>();
        pos = transform.position;
        sRenderer = GetComponent<SpriteRenderer>();
        if (startLeft)
            {
            sRenderer.sprite = rightSprite;
            }
        else
            {
            sRenderer.sprite = leftSprite;
            }
	}
	
	// Update is called once per frame
	void Update () {
        if (MicrophoneScript.command == "Left")
            {
            left = true;
            right = false;
            }
        else if (MicrophoneScript.command == "Right")
            {
            left = false;
            right = true;
            }

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
            if (startLeft)
                {
                Vector3 targetPos = new Vector3(pos.x + offset, pos.y);

                if (left)
                    {
                    transform.position = Vector3.Lerp(transform.position, pos, moveSpeed * Time.deltaTime);
                    }
                if (right)
                    {
                    transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
                    }
                }

            if (startRight)
                {
                Vector3 targetPos = new Vector3(pos.x - offset, pos.y);

                if (left)
                    {
                    transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
                    }
                if (right)
                    {
                    transform.position = Vector3.Lerp(transform.position, pos, moveSpeed * Time.deltaTime);
                    }
                }
            }
	}

    }

