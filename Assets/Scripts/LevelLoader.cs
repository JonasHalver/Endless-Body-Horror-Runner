using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    private GameObject player;
    private Collider2D col;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider2D>();
        //player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.player != null)
            {
            if (col.IsTouching(GameManager.player.GetComponent<Collider2D>()))
                {
                GameManager.checkpointIndex = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }

    //private void OnTriggerEnter2D(Collider2D collision)
    //    {
    //    if (collision == player)
    //        {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //        }
    //    }
    }
