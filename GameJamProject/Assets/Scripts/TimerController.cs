using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public Text timerTexT;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad >= 5.0f)
            timerTexT.text = "Time since last Reboot: " + Mathf.Round(Time.timeSinceLevelLoad);
	}
}
