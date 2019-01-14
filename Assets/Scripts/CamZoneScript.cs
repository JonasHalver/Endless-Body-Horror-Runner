using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoneScript : MonoBehaviour {

    private GameObject player, b1, b2, b3, b4;
    private Collider2D col;

    public bool closeTop, closeBottom, closeLeft, closeRight;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<Collider2D>();
        b1 = transform.GetChild(0).gameObject;
        b2 = transform.GetChild(1).gameObject;
        b3 = transform.GetChild(2).gameObject;
        b4 = transform.GetChild(3).gameObject;
        }

    // Update is called once per frame
    void Update () {
        if (GameManager.player != null)
            {
            if (col.IsTouching(GameManager.player.GetComponent<Collider2D>()))
                {
                CameraController.currentZone = gameObject;
                if (closeTop)
                    {
                    b1.SetActive(true);
                    }
                if (closeBottom)
                    {
                    b2.SetActive(true);
                    }
                if (closeLeft)
                    {
                    b3.SetActive(true);
                    }
                if (closeRight)
                    {
                    b4.SetActive(true);
                    }
                GameManager.checkpointIndex = transform.Find("Checkpoint").GetComponent<Checkpoint>().checkpointIndex;
                }
            else
                {
                b1.SetActive(false);
                b2.SetActive(false);
                b3.SetActive(false);
                b4.SetActive(false);
                }
            }
	}
}
