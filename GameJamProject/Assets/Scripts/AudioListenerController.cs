using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioListenerController : MonoBehaviour {

    public AudioListener audioListener;
    public Toggle audioMuteToggle;

	// Use this for initialization
	void Start () {
        audioMuteToggle.isOn = !AudioManager.instance.isAudioEnabled;

        if (audioMuteToggle.isOn)
            AudioListener.volume = 0.0f;
        else
            AudioListener.volume = 1.0f;

       
        AudioManager.instance.isAudioEnabled = !audioMuteToggle.isOn;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeAudioState()
    {
        if (audioMuteToggle.isOn)
            AudioListener.volume = 0.0f;
        else
            AudioListener.volume = 1.0f;

        AudioManager.instance.isAudioEnabled = !AudioManager.instance.isAudioEnabled;
    }
}
