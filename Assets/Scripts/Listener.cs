using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour {

    //private Vector3 startPos;
    //public Vector3 targetPos;

    public bool activate = false;
    public bool hide = false;

    private SpriteRenderer sRenderer;

    public float threshold = 0.7f;
    private bool deafened = false;
    private bool withinEarshot;
    private Collider2D col;
    public Collider2D earshotCol;
    public Collider2D listenerCol;

    public Sprite sleepSprite;
    public Sprite wokeSprite;
    public Sprite activeSprite;
    public Sprite deactiveSprite;

    public float volumeDebug;

    // Use this for initialization
    void Start () {
        //startPos = transform.position;
        sRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        
        earshotCol = GameObject.Find("Earshot").GetComponent<Collider2D>();
        col.enabled = hide;
	}
	
	// Update is called once per frame
	void Update () {
        volumeDebug = MicrophoneScript.volume;

        if (listenerCol.IsTouching(earshotCol))
            {
            withinEarshot = true;
            Debug.Log(gameObject.name + "is within earshot");
            }
        else if (!listenerCol.IsTouching(earshotCol))
            {
            withinEarshot = false;
            }

        if (withinEarshot)
            {
            if (!deafened)
                {
                if (MicrophoneScript.volume > threshold)
                    {
                    StartCoroutine(Deafen());
                    deafened = true;
                    }
                }
            }
	}

    IEnumerator Deafen()
        {
        if (activate)
            {
            col.enabled = true;
            sRenderer.sprite = wokeSprite;
            yield return new WaitForSeconds(2f);
            col.enabled = false;
            sRenderer.sprite = sleepSprite;
            }

        if (hide)
            {
            col.enabled = false;
            sRenderer.sprite = deactiveSprite;
            yield return new WaitForSeconds(2f);
            col.enabled = true;
            sRenderer.sprite = activeSprite;
            }
        deafened = false;
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
    }

