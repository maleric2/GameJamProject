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
        Application.LoadLevel(1);
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
}
