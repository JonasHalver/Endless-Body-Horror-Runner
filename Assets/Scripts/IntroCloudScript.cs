using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCloudScript : MonoBehaviour {

    public GameObject bubble1, bubble2, bubble3, bubble4, menu;
    private Animator anim;
    private int bubbleIndex = 0;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        StartCoroutine(Intro1());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
            {
            switch (bubbleIndex)
                {
                case 1:
                    bubble1.SetActive(false);
                    bubble2.SetActive(true);
                    bubbleIndex++;
                    break;
                case 2:
                    bubble2.SetActive(false);
                    bubble3.SetActive (true);
                    bubbleIndex++;
                    break;
                case 3:
                    bubble3.SetActive (false);
                    bubble4.SetActive (true);
                    bubbleIndex++;
                    break;
                case 4:
                    StartCoroutine(Intro2());
                    break;
                }
            }
	}

    IEnumerator Intro1()
        {
        yield return new WaitForSeconds(1f);
        anim.SetBool("appear", true);
        yield return new WaitForSeconds(1f);
        bubble1.SetActive(true);
        bubbleIndex++;
        }

    IEnumerator Intro2()
        {
        bubble4.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("appear", false);
        yield return new WaitForSeconds(1f);
        menu.SetActive(true);
        }
}
