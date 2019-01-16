using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private Text txt;

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = GameManager.score.ToString();
	}
}
