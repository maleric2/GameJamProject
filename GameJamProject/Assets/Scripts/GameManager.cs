﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;

    public static float finalScore = 0.0f;
    public static float healBotKillCount = 0.0f;
    public static float fightBotKillCount = 0.0f;

    public Text killCount;

    [Tooltip("If not set, the player will default to the gameObject tagged as Player.")]
    public GameObject player;

    public enum gameStates { Playing, Death, GameOver, BeatLevel, WaitingWave};
    public gameStates gameState = gameStates.Playing;

    public List<Transform> targets;
    public int score = 0;
    public bool canBeatLevel = false;
    public int beatLevelScore = 0;

    public GameObject mainCanvas;
    public Text mainScoreDisplay;
    public GameObject gameOverCanvas;
    public Text gameOverScoreDisplay;

    public GameObject newWaveCanvas;
    public Text newWaveTextDisplay;

    [Tooltip("Only need to set if canBeatLevel is set to true.")]
    public GameObject beatLevelCanvas;

    public AudioSource backgroundMusic;
    public AudioClip gameOverSFX;

    [Tooltip("Only need to set if canBeatLevel is set to true.")]
    public AudioClip beatLevelSFX;

    private Health playerHealth;
    public int targetsAlive;
    //private float targetHealth=100;

    void Start()
    {
        Time.timeScale = 1.0f;

        if (gm == null)
            gm = gameObject.GetComponent<GameManager>();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (targets != null)
        {
            targetsAlive = targets.Count;
        }

        //playerHealth = player.GetComponent<Health>();

        // setup score display
        //Collect (0);

        // make other UI inactive
        //gameOverCanvas.SetActive (false);
        if (canBeatLevel)
            beatLevelCanvas.SetActive(false);
    }

    void Update()
    {
        killCount.text = "Kill Count\n <color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">HealBots:</color> " + healBotKillCount + "\n <color=#" + ColorUtility.ToHtmlStringRGB(Color.green) + ">FightBots:</color> " + fightBotKillCount;

        if (targets.Count == 0)
        {
            gameState = gameStates.GameOver;
        }

        switch (gameState)
        {
            case gameStates.Playing:
                newWaveCanvas.SetActive(false);
                if (!isAnyTarget())
                {
                    // update gameState
                    gameState = gameStates.Death;
                }
                /*if (playerHealth.isAlive == false)
				{
					// update gameState
					gameState = gameStates.Death;

					// set the end game score
					gameOverScoreDisplay.text = mainScoreDisplay.text;

					// switch which GUI is showing		
					mainCanvas.SetActive (false);
					gameOverCanvas.SetActive (true);
				} else if (canBeatLevel && score>=beatLevelScore) {
					// update gameState
					gameState = gameStates.BeatLevel;

					// hide the player so game doesn't continue playing
					player.SetActive(false);

					// switch which GUI is showing			
					mainCanvas.SetActive (false);
					beatLevelCanvas.SetActive (true);
				}*/
                break;
            case gameStates.WaitingWave:
                newWaveCanvas.SetActive(true);
                newWaveTextDisplay.text = "Wave " + (WaveManager.wm.currentWave+1) + " incoming";
                break;
            case gameStates.Death:
                if (backgroundMusic != null)
                {
                    backgroundMusic.volume -= 0.01f;
                    if (backgroundMusic.volume <= 0.0f)
                    {
                        AudioSource.PlayClipAtPoint(gameOverSFX, gameObject.transform.position);

                        gameState = gameStates.GameOver;
                    }
                }
                break;
            case gameStates.BeatLevel:
                if (backgroundMusic != null)
                {
                    backgroundMusic.volume -= 0.01f;
                    if (backgroundMusic.volume <= 0.0f)
                    {
                        AudioSource.PlayClipAtPoint(beatLevelSFX, gameObject.transform.position);

                        gameState = gameStates.GameOver;
                    }
                }
                break;
            case gameStates.GameOver:
                gameOverCanvas.SetActive(true);
                gameOverScoreDisplay.text = "Other computers infected: " + Mathf.Round(finalScore * Time.timeSinceLevelLoad);
                mainCanvas.GetComponent<TimerController>().enabled = false;
                backgroundMusic.pitch = 0.2f;

                Time.timeScale = 0.0f;
                break;
        }

    }


    public void Collect(int amount)
    {
        score += amount;
        if (canBeatLevel)
        {
            mainScoreDisplay.text = score.ToString() + " of " + beatLevelScore.ToString();
        }
        else
        {
            mainScoreDisplay.text = score.ToString();
        }

    }
    public bool isTargets()
    {
        if (targets.Count > 0)
        {
            return true;
        }
        return false;
    }
    

    public Transform GetRandomTarget()
    {
        if (isTargets())
        {
            int randTarget = (int)Random.Range(0, targets.Count);
            return targets[randTarget];
        }
        return null;

    }
    public bool isAnyTarget()
    {
        bool isAnyTarget = false;
        //if (targetsAlive > 0) isAnyTarget = true;
        int ofTargets = 0;
        foreach (Transform t in targets)
        {
            if (t != null)
            {
                isAnyTarget = true;
                ofTargets++;
            }
        }
        targetsAlive = ofTargets;
        return isAnyTarget;
    }
    public void TakeDownTarget()
    {
        //targetsAlive--;
    }
}
