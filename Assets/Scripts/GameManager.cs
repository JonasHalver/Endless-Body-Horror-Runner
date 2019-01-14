using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameObject currentCheckpoint;
    public static List<GameObject> checkpoints = new List<GameObject>();
    public static int checkpointIndex = 1;

    public static GameObject player;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
            {
            if (Input.GetButtonDown("Reset"))
                {
                StartCoroutine(Spawner());
                Debug.Log("Spawning New Player");
                }
            }
        else
            {
            if (Input.GetButtonDown("Reset"))
                {
                Debug.Log("Cannot Spawn New, " + player.name + " exists");
                }
            }


        }

    private void OnEnable()
        {
        SceneManager.sceneLoaded += SceneLoaded;
        }

    private void OnDisable()
        {
        SceneManager.sceneLoaded -= SceneLoaded;
        }

    void SceneLoaded(Scene scene, LoadSceneMode mod)
        {
        StartCoroutine(Spawner());
        }

    IEnumerator Spawner() {
        Debug.Log(checkpointIndex);
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject checkpoint in checkpoints)
            {
            if (checkpoint != null)
                {
                if (checkpointIndex == checkpoint.GetComponent<Checkpoint>().checkpointIndex)
                    {
                    currentCheckpoint = checkpoint;
                    Debug.Log("Right Checkpoint");
                    }
                else
                    {
                    Debug.Log("Wrong checkpoint");
                    }
                }
            }

        if (currentCheckpoint != null)
            {
            currentCheckpoint.SendMessage("SpawnPlayer");
            }
        yield return new WaitForSeconds(0.1f);
        checkpoints = new List<GameObject>();
        }
}
