using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioListenerController : MonoBehaviour {

    public AudioListener audioListener;
    public Toggle audioMuteToggle;

	// Use this for initialization
	void Start () {
        audioMuteToggle.isOn = !AudioManager.instance.isAudioEnabled;
        audioListener.enabled = !audioMuteToggle.isOn;
        AudioManager.instance.isAudioEnabled = audioListener.enabled;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeAudioState()
    {
        audioListener.enabled = !AudioManager.instance.isAudioEnabled;
        AudioManager.instance.isAudioEnabled = !AudioManager.instance.isAudioEnabled;
    }
}
