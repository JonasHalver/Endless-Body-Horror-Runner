using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIScript : MonoBehaviour {

    private UnityEngine.UI.Text text;

    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public bool stop;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<UnityEngine.UI.Text>();
	}

    // Update is called once per frame
    void Update()
        {
        if (MicrophoneScript.command != null)
            {
            text.text = MicrophoneScript.command;
            }

        else
            {
            text.text = null;
            }
        }

}
