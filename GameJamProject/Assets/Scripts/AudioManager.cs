using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;

    public bool isAudioEnabled;

	// Use this for initialization
	void Awake () {

        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
