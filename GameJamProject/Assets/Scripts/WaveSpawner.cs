using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{

    private float secondsBetweenSpawning;
    private float secondsBetweenWaves = 20.0f;
    public int currentWave = 1;

    public float savedTime = 0f;
    private int startWaveNumber = 10;
    private int waveIncrement = 5;

    private bool doWaveSpawning = true;
    public int currentWaveSpawner = 0;
    private GameManager gameManager;
    private WaveManager waveManager;
    // Use this for initialization
    void Start()
    {
        
        gameManager = GameObject.FindObjectOfType<GameManager>();
        waveManager = GameObject.FindObjectOfType<WaveManager>();
        secondsBetweenSpawning = waveManager.secondsBetweenSpawning;
        startWaveNumber = waveManager.startWaveNumber;
        secondsBetweenWaves = waveManager.secondsBetweenWaves;
        waveIncrement = waveManager.waveIncrement;
    }
    public Transform GetWaveTarget()
    {
        return waveManager.GetWaveTarget();
    }
    public void makeWave()
    {
        savedTime = Time.time; // store for next spawn
        startWaveNumber += waveIncrement;
        currentWave++;
        currentWaveSpawner = 1;
        waveManager.makeWave(currentWave);
        //Debug.Log("Get ready for wave " + currentWave);
    }
    public bool doWaveSpawn()
    {
        //if new wave and this return true
        if (currentWaveSpawner >= startWaveNumber)
        {
            gameManager.gameState = GameManager.gameStates.WaitingWave;
            if (Time.time - savedTime >= secondsBetweenWaves)
            {
                gameManager.gameState = GameManager.gameStates.Playing;
                makeWave();
                //secondsBetweenSpawning = secondsBetweenSpawning * 0.8f;
                return true;
            }
        }
        else
        {
            if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
            {
                savedTime = Time.time; // store for next spawn
                currentWaveSpawner++;
                return true;
            }
            
        }
        return false;
    }

    void SetWaitingWaveState()
    {
        if(gameManager.gameState!=GameManager.gameStates.WaitingWave)
        gameManager.gameState = GameManager.gameStates.WaitingWave;
    }

}
