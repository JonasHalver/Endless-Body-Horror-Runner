using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIobjectScript : MonoBehaviour {

    public Sprite spriteSilent, spriteLoud;
    private Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (MicrophoneScript.volume > GameManager.speakThreshold)
            {
            image.sprite = spriteLoud;
            }
        else
            {
            image.sprite = spriteSilent;
            }
	}
}
