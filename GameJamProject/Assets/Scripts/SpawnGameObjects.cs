using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour
{

    public GameObject spawnPrefab;
    private GameManager gameManager;

    public float minSecondsBetweenSpawning = 3.0f;
    public float maxSecondsBetweenSpawning = 6.0f;

    private float savedTime;
    private float secondsBetweenSpawning;
    public int startWaveNumber = 10;
    public int waveIncrement = 5;

    private bool doWaveSpawning = true;
    private int currentWaveSpawner = 0;

    // Use this for initialization
    void Start()
    {
        savedTime = Time.time;
        secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentWaveSpawner >= startWaveNumber)
        {
            startWaveNumber += waveIncrement;
            gameManager.currentWave++;
            Debug.Log("Get ready for wave " + gameManager.currentWave);
            doWaveSpawning = false;
            currentWaveSpawner = 0;
        }
        else
        {
            MakeWave();
        }
        if (Time.time - savedTime >= gameManager.secondsBetweenWaves) // is it time for new wave?
        {
            doWaveSpawning = true;
        }
    }
    void MakeWave()
    {
        if (doWaveSpawning)
            if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
            {
                MakeThingToSpawn();
                savedTime = Time.time; // store for next spawn
                secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
                currentWaveSpawner++;
            }
    }
    void MakeThingToSpawn()
    {
        // create a new gameObject
        GameObject clone = Instantiate(spawnPrefab, transform.position, transform.rotation) as GameObject;

        // set chaseTarget if specified
        //TODO chose random target form targets in gameManager
        if ((gameManager != null) && (gameManager.isTargets()) && (clone.gameObject.GetComponent<Chaser>() != null))
        {
            clone.gameObject.GetComponent<Chaser>().SetTarget(gameManager.GetWaveTarget());
        }
    }
}
