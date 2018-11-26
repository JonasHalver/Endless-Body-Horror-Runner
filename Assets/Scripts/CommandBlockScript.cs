using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBlockScript : MonoBehaviour {

    public Vector3 leftPos;
    public Vector3 rightPos;

    public float moveSpeed = 1f;

    private bool left;
    private bool right;

    private bool withinEarshot;
    private Collider2D col;
    public Collider2D earshotCol;

    void Start () {
        col = gameObject.GetComponent<Collider2D>();
        earshotCol = GameObject.Find("Earshot").GetComponent<Collider2D>();
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
            if (left)
                {
                transform.position = Vector3.Lerp(transform.position, leftPos, moveSpeed * Time.deltaTime);
                }
            if (right)
                {
                transform.position = Vector3.Lerp(transform.position, rightPos, moveSpeed * Time.deltaTime);
                }
            }
	}

    }

