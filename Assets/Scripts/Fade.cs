using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    private UnityEngine.UI.Image img;

	// Use this for initialization
	void Start () {
        img = GetComponent<UnityEngine.UI.Image>();
        StartCoroutine(FadeIn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeIn()
        {
        for (float f = 1f; f > 0; f = f - 0.02f)
            {
            img.color = new Color(0, 0, 0, f);
            yield return new WaitForEndOfFrame();
            }
        }
}
