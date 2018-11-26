using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Windows.Speech;
using System.Linq;

public class MicrophoneScript : MonoBehaviour {

    public string micName;
    public AudioSource audioSource;
    public AudioClip micAudio;
    public int samples = 128;

    public AudioMixerGroup mixMic;
    public AudioMixerGroup master;

    public static float volume;
    public float sensitivity = 10;

    public Camera cam;

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public Color blue;
    public Color red;

    public static bool up = false;
    public static bool down = false;
    public static bool left = false;
    public static bool right = false;
    public static bool stop = false;

    public static string command;

    public List<bool> commands = new List<bool>();

    void Start () {
        micName = Microphone.devices[0].ToString();
        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.clip = Microphone.Start(micName, true, 10, 44100);
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
        audioSource.outputAudioMixerGroup = mixMic;

        commands.Add(up);
        commands.Add(down);
        commands.Add(left);
        commands.Add(right);
        commands.Add(stop);

        //Create keywords for keyword recognizer
        keywords.Add("red", () =>
        {
            cam.backgroundColor = red;
        });
        keywords.Add("blue", () =>
        {
            cam.backgroundColor = blue;
        });
        keywords.Add("up", () =>
        {
            command = "Up";
        });
        keywords.Add("down", () =>
        {
            command = "Down";
        });
        keywords.Add("left", () =>
        {
            command = "Left";
        });
        keywords.Add("right", () =>
        {
            command = "Right";
        });
        //keywords.Add("stop", () =>
        //{
        //    command = "Stop";
        //});
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        keywordRecognizer.Start();
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

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))
            {
            keywordAction.Invoke();
            }
        }
    }
