using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour {

    //private Vector3 startPos;
    //public Vector3 targetPos;

    public bool activateOnSound = false;
    public bool hideOnSound = false, isVertical = false, isHorizontal = false;

    private SpriteRenderer sRenderer;

    public float threshold = 0.7f;
    private bool deafened = false;
    private bool withinEarshot;
    private Collider2D col;
    public Collider2D earshotCol;
    public Collider2D listenerCol;

    public Sprite activatorActiveV, activatorActiveH;
    public Sprite activatorDeactiveV, activatorDeactiveH;
    public Sprite hiderActiveV, hiderActiveH;
    public Sprite hiderDeactiveV, hiderDeactiveH;
    private Sprite loudSprite, quietSprite;

    public float delay = 1f;
    public float volumeDebug;

    // Use this for initialization
    void Start () {
        //startPos = transform.position;
        sRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        
        earshotCol = GameObject.Find("Earshot").GetComponent<Collider2D>();
        col.enabled = hideOnSound;

        if (hideOnSound)
            {
            if (isVertical)
                {
                loudSprite = hiderDeactiveV;
                quietSprite = hiderActiveV;
                }
            if (isHorizontal)
                {
                loudSprite = hiderDeactiveH;
                quietSprite = hiderActiveH;
                }
            }
        if (activateOnSound)
            {
            if (isVertical)
                {
                loudSprite = activatorActiveV;
                quietSprite = activatorDeactiveV;
                }
            if (isHorizontal)
                {
                loudSprite = activatorActiveH;
                quietSprite = activatorDeactiveH;
                }
            }
        sRenderer.sprite = quietSprite;
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
        if (activateOnSound)
            {
            col.enabled = true;
            sRenderer.sprite = loudSprite;
            yield return new WaitForSeconds(delay);
            col.enabled = false;
            sRenderer.sprite = quietSprite;
            }

        if (hideOnSound)
            {
            col.enabled = false;
            sRenderer.sprite = loudSprite;            
            yield return new WaitForSeconds(delay);
            col.enabled = true;
            sRenderer.sprite = quietSprite;
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

