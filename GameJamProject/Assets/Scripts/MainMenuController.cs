using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    public GameObject mainCanvas;
    public GameObject creditsCanvas;
    public GameObject howToPlayCanvas;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame(){
		Application.LoadLevel (1);
	}
    public void LoadMenu()
    {
        howToPlayCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
    public void HowToPlay()
    {
        mainCanvas.SetActive(false);
        howToPlayCanvas.SetActive(true);
    }

    public void ReturnToMenu()
    {
        Application.LoadLevel(0);
    }

    public void RestartLevel()
    {
        Application.LoadLevel(1);
    }

    public void Credits()
    {
        mainCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
