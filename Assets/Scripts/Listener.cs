using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour {

    private Vector3 normalPos;
    public Vector3 hidePos;

    public bool move = false;
    public bool hide = false;

    private SpriteRenderer sRenderer;
    private Collider2D col;

    public float threshold = 0.7f;
    private bool deafened = false;

	// Use this for initialization
	void Start () {
        normalPos = transform.position;
        sRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!deafened)
            {
            if (MicrophoneScript.volume > threshold)
                {
                StartCoroutine(Deafen());
                deafened = true;
                }
            }
	}

    IEnumerator Deafen()
        {
        if (move)
            {
            transform.position = hidePos;
            yield return new WaitForSeconds(2f);
            transform.position = normalPos;
            }

        if (hide)
            {
            col.enabled = false;
            sRenderer.enabled = false;
            yield return new WaitForSeconds(2f);
            col.enabled = true;
            sRenderer.enabled = true;
            }
        deafened = false;
        }
}
