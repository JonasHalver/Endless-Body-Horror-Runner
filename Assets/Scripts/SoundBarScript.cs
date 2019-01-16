using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBarScript : MonoBehaviour {

    public Color quiet, speak, shout;
    private GameObject fill;
    private Image fillImage;

	// Use this for initialization
	void Start () {
        fill = transform.Find("Fill Area").transform.Find("Fill").gameObject;
        fillImage = fill.GetComponent<Image>();
        StartCoroutine(Volume());
	}
	
	// Update is called once per frame
	void Update () {
        //transform.localScale = new Vector3(MicrophoneScript.volume, 1, 1);
        //GetComponent<Slider>().value = MicrophoneScript.volume;

        if (GetComponent<Slider>().value >= GameManager.shoutThreshold)
            {
            fillImage.color = shout;
            }
        else if (GetComponent<Slider>().value > GameManager.speakThreshold && GetComponent<Slider>().value < GameManager.shoutThreshold)
            {
            fillImage.color = speak;
            }
        else
            {
            fillImage.color = quiet;
            }
	}

    IEnumerator Volume()
        {
        float vol1, vol2;
        while (true)
            {
            vol1 = MicrophoneScript.volume;
            yield return new WaitForSeconds(0.1f);
            vol2 = MicrophoneScript.volume;
            GetComponent<Slider>().value = Mathf.Clamp((vol1+vol2)/2, GetComponent<Slider>().value - 0.2f, GetComponent<Slider>().value + 0.2f);
            }
        }
}
