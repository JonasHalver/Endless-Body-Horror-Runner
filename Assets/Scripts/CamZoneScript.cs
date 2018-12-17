using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoneScript : MonoBehaviour {

    private GameObject player;
    private Collider2D col;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (col.IsTouching(player.GetComponent<Collider2D>()))
            {
            CameraController.currentZone = gameObject;
            }
	}
}
