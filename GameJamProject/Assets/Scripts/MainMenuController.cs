using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewGame()
    {
		Invoke ("StartGame", 1.0f);       
    }

	public void StartGame(){
		Application.LoadLevel (1);
	}
    public void LoadMenu()
    {
        Application.LoadLevel(0);
    }
    public void HowToPlay()
    {
        Application.LoadLevel(2);
    }
    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Credits()
    {
        //TO DO
        // implement Credits canvas and switch controller
    }

    public void Exit()
    {
        Application.Quit();
    }

	public void AudioStart(){
		gameObject.GetComponent<AudioSource>().Play();

	}
}
