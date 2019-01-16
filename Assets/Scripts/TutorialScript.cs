using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

    private GameObject player, bubble;
    private TextMesh txt;
    private float distance;
    private SpriteRenderer sRenderer, sRendererBubble;
    public Sprite bubbleSprite;
    //private string name;

	// Use this for initialization
	void Start () {
        sRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        bubble = transform.Find("SpeechBubble").gameObject;
        sRendererBubble = bubble.GetComponent<SpriteRenderer>();
        txt = bubble.transform.Find("Text").GetComponent<TextMesh>();

        sRendererBubble.sprite = bubbleSprite;

        //name = transform.name;
        //
        //switch (name)
        //    {
        //    case "CloudDaddy1":
        //        txt.text = "In this world you \n can use your voice \n to interact with different \n things. Try jumping up on \n that cloud over there...";
        //        break;
        //    case "CloudDaddy2":
        //        txt.text = "Some clouds are sentient \n and will do your bidding. \n To command it, simply \n tell it to go up.";
        //        break;
        //    case "CloudDaddy3":
        //        txt.text = "These dots \n looks delicious...";
        //        break;
        //    case "CloudDaddy4":
        //        txt.text = "Over there! The door \n to the next part \n of the world! \n go to it my son!";
        //        break;
        //    case "CloudDaddy5":
        //        txt.text = "Different clouds have \n different colors. They \n will appear depending on \n whether you are silent, \n talking or yelling.";
        //        break;
        //    case "CloudDaddy6":
        //        txt.text = "Some objects can change \n shape and size \n if you yell at them.";
        //        break;
        //    }
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.player != null)
            {
            distance = Vector3.Distance(transform.position, GameManager.player.transform.position);
            }

        if (distance > 10f)
            {
            sRenderer.color = new Color(1, 1, 1, 0);
            bubble.SetActive(false);
            }
        else if (distance < 10f && distance > 5f)
            {
            sRenderer.color = new Color(1, 1, 1, 1 - (distance / 10));
            }
        else
            {
            sRenderer.color = new Color(1, 1, 1, 1);
            bubble.SetActive(true);
            }
        }
}
