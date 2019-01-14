using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public GameObject player;
    public int checkpointIndex;

	// Use this for initialization
	void Start () {
        GameManager.checkpoints.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnPlayer()
        {
        //GameObject newPlayer;
        GameManager.player = Instantiate(player, transform.position, transform.rotation);
        }
}
