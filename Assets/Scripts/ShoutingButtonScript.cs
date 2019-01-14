using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoutingButtonScript : MonoBehaviour {

    private GameObject player;
    public GameObject otherCloud;
    private SpriteRenderer sRendererBG, sRenderer;
    private SoundReceiver sr;
    public float distance;
    public enum ButtonType { Play, Exit }
    public ButtonType bType;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        sRendererBG = otherCloud.GetComponent<SpriteRenderer>();
        sRenderer = GetComponent<SpriteRenderer>();
        sr = gameObject.GetComponent<SoundReceiver>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.player != null)
            {
            distance = Vector3.Distance(transform.position, GameManager.player.transform.position);
            }
        if (distance > 4.5f)
            {
            sRendererBG.color = new Color(1, 1, 1, 0);
            sr.enabled = false;
            }
        else
            {
            sRendererBG.color = new Color(1, 1, 1, 0.45f - (distance/10));
            sr.enabled = true;
            }

        if (sRenderer.size.x > sRendererBG.size.x)
            {
            switch (bType)
                {
                case ButtonType.Play:
                    Play();
                    break;
                case ButtonType.Exit:
                    Exit();
                    break;
                }
            }

        if (MicrophoneScript.command == "Exit")
            {
            switch (bType)
                {
                case ButtonType.Play:
                    //Play();
                    break;
                case ButtonType.Exit:
                    Exit();
                    break;
                }
            }
        if (MicrophoneScript.command == "Play")
            {
            switch (bType)
                {
                case ButtonType.Play:
                    Play();
                    break;
                case ButtonType.Exit:
                    //Exit();
                    break;
                }
            }
        }

    public void Play()
        {
        SceneManager.LoadScene("Level_1");
        }

    public void Exit()
        {
        Application.Quit();
        }
}
