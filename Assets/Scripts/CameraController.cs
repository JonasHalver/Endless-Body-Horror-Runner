using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public Collider2D col;

    public float camSpeed = 0.5f;
    
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!col.IsTouching(player.GetComponent<Collider2D>()))
            {
            if (player.transform.position.x > transform.position.x)
                {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, 1 * Time.deltaTime), 1, -10);
                }
            }
        else
            {
            transform.position = new Vector3(transform.position.x + (camSpeed * Time.deltaTime), 1, -10);
            }
	}
}
