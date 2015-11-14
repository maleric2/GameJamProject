using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour
{

    public GameObject spawnPrefab;
    private GameManager gameManager;
    private WaveManager waveManager;

    //public float minSecondsBetweenSpawning = 3.0f;
    //public float maxSecondsBetweenSpawning = 6.0f;

    //private float savedTime;
    //private float secondsBetweenSpawning;


    // Use this for initialization
    void Start()
    {
        //savedTime = Time.time;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        waveManager = GameObject.FindObjectOfType<WaveManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        waveManager.makeWave();
        MakeWave();
        /*if (currentWaveSpawner >= startWaveNumber)
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
        }*/
    }
    void MakeWave()
    {
        if (waveManager.doWaveSpawn())
                MakeThingToSpawn();

    }
    void MakeThingToSpawn()
    {
        // create a new gameObject
        GameObject clone = Instantiate(spawnPrefab, transform.position, transform.rotation) as GameObject;

        // set chaseTarget if specified
        //TODO chose random target form targets in gameManager
        if ((gameManager != null) && (gameManager.isTargets()) && (clone.gameObject.GetComponent<Chaser>() != null))
        {
            clone.gameObject.GetComponent<Chaser>().SetTarget(waveManager.GetWaveTarget());
        }
    }
}
