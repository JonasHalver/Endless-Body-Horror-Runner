using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MicrophoneScript : MonoBehaviour {

    public string micName;
    public AudioSource audioSource;
    public AudioClip micAudio;
    public int samples = 128;

    public AudioMixerGroup mixMic;
    public AudioMixerGroup master;

    public static float volume;
    public float sensitivity = 10;

	void Start () {
        micName = Microphone.devices[0].ToString();
        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.clip = Microphone.Start(micName, true, 10, 44100);
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
        audioSource.outputAudioMixerGroup = mixMic;
	}
	
	// Update is called once per frame
	void Update () {
        volume = Mathf.Clamp(Loudness() * sensitivity, 0, 1);
        }


    public float Loudness()
        {
        float level = 0;
        float[] soundData = new float[samples];
        audioSource.GetOutputData(soundData, 0);
        foreach (float i in soundData)
            {
            level += Mathf.Abs(i);
            }
        return level / samples;
        }
}
