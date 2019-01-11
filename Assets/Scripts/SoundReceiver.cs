using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReceiver : MonoBehaviour {

    public enum BlockType { Activate, Hide, Grow, Move, Rotate, Command }
    public BlockType type = BlockType.Activate;
    public bool triggerOnSpeaking;

    private SpriteRenderer srend;
    private BoxCollider2D col;
    public Sprite onSprite, offSprite;

    public float shoutThreshold = 0.75f, speakThreshold = 0.15f, delay = 1f;
    private bool receiveInput = true;

    public Vector2 targetScale;
    private Vector2 colSize, spriteSize;
    public float changeSpeed = 1f;
    private Quaternion startRot;
    public float direction = 1f;

    private CommandBlockScript cmdScript;

    // Use this for initialization
    void Start () {
        cmdScript = GetComponent<CommandBlockScript>();
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
            case BlockType.Command:
                break;
            }
	}
	
	// Update is called once per frame
	void Update () {
		if (receiveInput)
            {
            if (MicrophoneScript.volume >= shoutThreshold)
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

        if (cmdScript != null)
            {
            if (MicrophoneScript.command == "Up")
                {
                cmdScript.dir = CommandBlockScript.Directions.Up;
                }
            if (MicrophoneScript.command == "Down")
                {
                cmdScript.dir = CommandBlockScript.Directions.Down;
                }
            if (MicrophoneScript.command == "Left")
                {
                cmdScript.dir = CommandBlockScript.Directions.Left;
                }
            if (MicrophoneScript.command == "Right")
                {
                cmdScript.dir = CommandBlockScript.Directions.Right;
                }
            if (MicrophoneScript.command == "Stop")
                {
                cmdScript.dir = CommandBlockScript.Directions.Stop;
                }
            if (MicrophoneScript.command == "Return")
                {
                cmdScript.SendMessage("Return");
                cmdScript.dir = CommandBlockScript.Directions.Stop;
                }
            }
        }

    IEnumerator OnSpeaking()
        {
        //receiveInput = false;
        while (true)
            {
            if (triggerOnSpeaking)
                {
                switch (type)
                    {
                    case BlockType.Activate:
                        srend.sprite = onSprite;
                        col.enabled = true;
                        break;
                    case BlockType.Hide:
                        srend.sprite = offSprite;
                        col.enabled = false;
                        break;
                    case BlockType.Grow:
                        break;
                    case BlockType.Move:
                        break;
                    case BlockType.Rotate:
                        break;
                    case BlockType.Command:
                        break;
                    }
                }
            yield return new WaitForSeconds(delay);
            if (MicrophoneScript.volume < speakThreshold)
                {
                break;
                }
            }
        switch (type)
            {
            case BlockType.Activate:
                srend.sprite = offSprite;
                col.enabled = false;
                break;
            case BlockType.Hide:
                srend.sprite = onSprite;
                col.enabled = true;
                break;
            case BlockType.Grow:
                break;
            case BlockType.Move:
                break;
            case BlockType.Rotate:
                break;
            case BlockType.Command:
                break;
            }
        receiveInput = true;
        }

    IEnumerator OnShouting()
        {
        receiveInput = false;
        while (true)
            {
            switch (type)
                {
                case BlockType.Activate:
                    srend.sprite = onSprite;
                    col.enabled = true;
                    //yield return new WaitForSeconds(delay);
                    //srend.sprite = offSprite;
                    //col.enabled = false;
                    break;
                case BlockType.Hide:
                    srend.sprite = offSprite;
                    col.enabled = false;
                    //yield return new WaitForSeconds(delay);
                    //srend.sprite = onSprite;
                    //col.enabled = true;
                    break;
                case BlockType.Grow:
                    break;
                case BlockType.Move:
                    break;
                case BlockType.Rotate:
                    break;
                case BlockType.Command:
                    break;
                }
            yield return new WaitForSeconds(delay);
            if (MicrophoneScript.volume < speakThreshold)
                {
                break;
                }
            }
        switch (type)
            {
            case BlockType.Activate:
                srend.sprite = offSprite;
                col.enabled = false;
                break;
            case BlockType.Hide:
                srend.sprite = onSprite;
                col.enabled = true;
                break;
            case BlockType.Grow:
                break;
            case BlockType.Move:
                break;
            case BlockType.Rotate:
                break;
            case BlockType.Command:
                break;
            }
                receiveInput = true;
        }

    public void Grow()
        {
        srend.size = new Vector2(Mathf.Lerp(srend.size.x, spriteSize.x + targetScale.x, changeSpeed * Time.deltaTime), Mathf.Lerp(srend.size.y, spriteSize.y + targetScale.y, changeSpeed * Time.deltaTime));
        col.size = new Vector2(Mathf.Lerp(col.size.x, colSize.x + targetScale.x, changeSpeed * Time.deltaTime), Mathf.Lerp(col.size.y, colSize.y + targetScale.y, changeSpeed * Time.deltaTime));
        }

    public void SizeReset()
        {
        srend.size = new Vector2(Mathf.Lerp(srend.size.x, spriteSize.x, changeSpeed * Time.deltaTime), Mathf.Lerp(srend.size.y, spriteSize.y, changeSpeed * Time.deltaTime));
        col.size = new Vector2(Mathf.Lerp(col.size.x, colSize.x, changeSpeed * Time.deltaTime), Mathf.Lerp(col.size.y, colSize.y, changeSpeed * Time.deltaTime));
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
