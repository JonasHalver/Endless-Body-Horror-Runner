﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.player.GetComponent<CharacterControllerScript>().isDead)
            {
            GetComponent<Image>().enabled = true;
            }
	}
}