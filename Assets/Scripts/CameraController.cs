using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    //public Collider2D col;
    //public bool movingCam = true;
    public static GameObject currentZone;

    public float camSpeed = 5f;
    
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (currentZone != null)
            {
            transform.position = Vector3.Lerp(transform.position, currentZone.transform.position, camSpeed * Time.deltaTime);
            }
	}
}
