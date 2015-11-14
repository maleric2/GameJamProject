using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {
    public int maxNumberOfWaves;
    public List<int> waveTargetNumber;

    public float secondsBetweenSpawning;
    public float secondsBetweenWaves = 20.0f;

    public float minSecondsBetweenSpawning = 3.0f;
    public float maxSecondsBetweenSpawning = 6.0f;
    public int currentWave = 1;

    public float savedTime=0f;
    public int startWaveNumber = 10;
    public int waveIncrement = 5;

    private bool doWaveSpawning = true;
    public int currentWaveSpawner = 0;
    public GameManager gameManager;
    // Use this for initialization
    void Start()
    {

        secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);

        gameManager = GameObject.FindObjectOfType<GameManager>();
        waveTargetNumber = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Transform GetWaveTarget()
    {
        if (currentWave == 0) currentWave = 1;
        if (waveTargetNumber.Count < (currentWave))
        {
            SetRandomTarget();
            currentWave = waveTargetNumber.Count;
        }
        return gameManager.targets[waveTargetNumber[currentWave - 1]];
    }
    private void SetRandomTarget()
    {
        bool defined = false;
        int targetNumber = Random.Range(0, gameManager.targets.Count);
        /*do
        {
            defined = true;
            targetNumber = Random.Range(0, gameManager.targets.Count);

            if (waveTargetNumber.Count > 0)
                foreach (int i in waveTargetNumber)
                {
                    if (i == targetNumber) defined = false;
                }

        } while (!defined);*/
        waveTargetNumber.Add(targetNumber);
        Debug.Log("New Wave Target defined");
    }
    public void makeWave()
    {
        if (currentWaveSpawner >= startWaveNumber)
        {
            startWaveNumber += waveIncrement;
            currentWave++;
            Debug.Log("Get ready for wave " + currentWave);
            doWaveSpawning = false;
            currentWaveSpawner = 0;
        }
    }
    public bool doWaveSpawn()
    {
        if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
        {
            savedTime = Time.time; // store for next spawn
            secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
            currentWaveSpawner++;
        }
            return false;
    }
}
