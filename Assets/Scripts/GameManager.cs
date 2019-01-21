using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameObject currentCheckpoint;
    public static List<GameObject> checkpoints = new List<GameObject>();
    public static int checkpointIndex = 1, score = 0;

    public static float shoutThreshold = 0.75f, speakThreshold = 0.1f;
    public static bool isShouting, isSpeaking, hasWon;

    public static GameObject player;

    public Slider speakSlider, shoutSlider;

    private AudioSource aSource;
    public AudioClip menuMusic, gameMusic;

	// Use this for initialization
	void Start () {
        aSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(transform.gameObject);
        aSource.PlayOneShot(menuMusic);
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
            {
            if (Input.GetButtonDown("Reset"))
                {
                StartCoroutine(Spawner());
                //Debug.Log("Spawning New Player");
                }
            }
        else
            {
            if (Input.GetButtonDown("Reset"))
                {
                //Debug.Log("Cannot Spawn New, " + player.name + " exists");
                }
            }

        if (speakSlider != null)
            {
            speakThreshold = speakSlider.value;
            }
        if (shoutSlider != null)
            {
            shoutThreshold = shoutSlider.value;
            }

        if (MicrophoneScript.volume >= shoutThreshold)
            {
            
            }
        else if (MicrophoneScript.volume >= speakThreshold && MicrophoneScript.volume < shoutThreshold)
            {

            }

        if (Input.GetKeyDown("page up"))
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        if (SceneManager.GetActiveScene().buildIndex != 0)
            {
            aSource.Stop();
            aSource.PlayOneShot(gameMusic);
            }
        }

    IEnumerator Spawner() {
        //Debug.Log(checkpointIndex);
        hasWon = false;
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject checkpoint in checkpoints)
            {
            if (checkpoint != null)
                {
                if (checkpointIndex == checkpoint.GetComponent<Checkpoint>().checkpointIndex)
                    {
                    currentCheckpoint = checkpoint;
                    //Debug.Log("Right Checkpoint");
                    }
                else
                    {
                    //Debug.Log("Wrong checkpoint");
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
