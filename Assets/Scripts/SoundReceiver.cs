﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReceiver : MonoBehaviour {

    public enum BlockType { Activate, Hide, Grow, Move, Rotate }
    public BlockType type = BlockType.Activate;
    public bool triggerOnSpeaking;

    private SpriteRenderer srend;
    private BoxCollider2D col;
    public Sprite onSprite, offSprite;

    public float shoutThreshold = 0.75f, speakThreshold = 0.15f, delay = 1f;
    private bool receiveInput = true;

    public Vector3 targetScale;
    private Vector2 colSize, spriteSize;
    public float changeSpeed = 1f;
    private Quaternion startRot;
    public float direction = 1f;

    // Use this for initialization
    void Start () {
        srend = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        startRot = transform.rotation;
        colSize = col.size;
        spriteSize = srend.size;

		switch (type)
            {
            case BlockType.Activate:
                srend.sprite = offSprite;
                col.enabled = false;
                break;
            case BlockType.Hide:
                srend.sprite = onSprite;
                break;
            case BlockType.Grow:
                break;
            case BlockType.Move:
                break;
            case BlockType.Rotate:
                break;
            }
	}
	
	// Update is called once per frame
	void Update () {
		if (receiveInput)
            {
            if (MicrophoneScript.volume > shoutThreshold)
                {
                StartCoroutine(OnShouting());
                }
            if (MicrophoneScript.volume > speakThreshold && MicrophoneScript.volume < shoutThreshold)
                {
                StartCoroutine(OnSpeaking());
                }
            }

        if (type == BlockType.Grow || type == BlockType.Rotate)
            {
            if (MicrophoneScript.volume >= shoutThreshold)
                {
                if (type == BlockType.Grow)
                    {
                    Grow();
                    }
                if (type ==  BlockType.Rotate)
                    {
                    Rotate();
                    }
                }
            else
                {
                if (type == BlockType.Grow)
                    {
                    SizeReset();
                    }
                if (type == BlockType.Rotate)
                    {
                    RotationReset();
                    }
                }

            if (triggerOnSpeaking)
                {
                if (MicrophoneScript.volume >= speakThreshold)
                    {
                    if (type == BlockType.Grow)
                        {
                        Grow();
                        }
                    if (type == BlockType.Rotate)
                        {
                        Rotate();
                        }
                    }
                else
                    {
                    if (type == BlockType.Grow)
                        {
                        SizeReset();
                        }
                    if (type == BlockType.Rotate)
                        {
                        RotationReset();
                        }
                    }
                }
            }
	}

    IEnumerator OnSpeaking()
        {
        receiveInput = false;
        if (triggerOnSpeaking)
            {
            switch (type)
                {
                case BlockType.Activate:
                    srend.sprite = onSprite;
                    col.enabled = true;
                    yield return new WaitForSeconds(delay);
                    srend.sprite = offSprite;
                    col.enabled = false;
                    break;
                case BlockType.Hide:
                    srend.sprite = offSprite;
                    col.enabled = false;
                    yield return new WaitForSeconds(delay);
                    srend.sprite = onSprite;
                    col.enabled = true;
                    break;
                case BlockType.Grow:
                    break;
                case BlockType.Move:
                    break;
                case BlockType.Rotate:
                    break;
                }
            }
        receiveInput = true;
        }

    IEnumerator OnShouting()
        {
        receiveInput = false;
        switch (type)
            {
            case BlockType.Activate:
                srend.sprite = onSprite;
                col.enabled = true;
                yield return new WaitForSeconds(delay);
                srend.sprite = offSprite;
                col.enabled = false;
                break;
            case BlockType.Hide:
                srend.sprite = offSprite;
                col.enabled = false;
                yield return new WaitForSeconds(delay);
                srend.sprite = onSprite;
                col.enabled = true;
                break;
            case BlockType.Grow:
                break;
            case BlockType.Move:
                break;
            case BlockType.Rotate:
                break;
            }
        receiveInput = true;
        }

    public void Grow()
        {
        srend.size = new Vector2(Mathf.Lerp(srend.size.x, spriteSize.x + targetScale.x, changeSpeed * Time.deltaTime), srend.size.y);
        col.size = new Vector2(Mathf.Lerp(col.size.x, (colSize.x + targetScale.x) - 3, changeSpeed * Time.deltaTime), col.size.y);
        }

    public void SizeReset()
        {
        srend.size = new Vector2(Mathf.Lerp(srend.size.x, spriteSize.x, changeSpeed * Time.deltaTime), srend.size.y);
        col.size = new Vector2(Mathf.Lerp(col.size.x, colSize.x, changeSpeed * Time.deltaTime), col.size.y);
        }

    public void Rotate()
        {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + ((direction * changeSpeed) * Time.deltaTime), transform.rotation.w);
        }

    public void RotationReset()
        {
        transform.rotation = Quaternion.Lerp(transform.rotation, startRot, changeSpeed * Time.deltaTime);
        }
}