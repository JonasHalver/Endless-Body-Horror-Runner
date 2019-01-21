using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    private GameObject player;
    private Collider2D col;
    public GameObject confetti;
    private bool isReadyToLoad;
    public GameObject endCloud, endBubble;
    private Animator cloudAnim;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider2D>();
        //confetti = GameObject.FindGameObjectWithTag("Confetti");
        //player = GameObject.FindGameObjectWithTag("Player");
        cloudAnim = endCloud.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.player != null)
            {
            if (col.IsTouching(GameManager.player.GetComponent<Collider2D>()))
                {
                StartCoroutine(NextLevel());
                }
            }

        if (isReadyToLoad)
            {
            if (Input.GetKeyDown("c") || MicrophoneScript.command == "Continue")
                {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }

    IEnumerator NextLevel()
        {
        GameManager.hasWon = true;
        GameManager.checkpointIndex = 1;
        confetti.SetActive(true);
        Destroy(GameManager.player);
        yield return new WaitForSeconds(0.5f);
        cloudAnim.SetBool("appear", true);
        yield return new WaitForSeconds(1f);
        endBubble.SetActive(true);
        isReadyToLoad = true;
        }
    }
