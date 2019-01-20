using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayScript : MonoBehaviour {

    private UnityEngine.UI.Text txt;
    private SpriteRenderer sRenderer;
    public float time = 20f;

	// Use this for initialization
	void Start () {
        txt = GetComponent<UnityEngine.UI.Text>();
        sRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Delay());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Delay()
        {
        yield return new WaitForSeconds(time);

        if (txt != null)
            {
            txt.enabled = true;
            }

        if (sRenderer != null)
            {
            sRenderer.enabled = true;
            }
        }
}
