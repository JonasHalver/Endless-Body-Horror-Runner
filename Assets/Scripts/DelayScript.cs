using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayScript : MonoBehaviour {

    private UnityEngine.UI.Text txt;
    public float time = 20f;

	// Use this for initialization
	void Start () {
        txt = GetComponent<UnityEngine.UI.Text>();
        StartCoroutine(Delay());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Delay()
        {
        yield return new WaitForSeconds(time);
        txt.enabled = true;
        }
}
