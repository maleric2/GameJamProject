using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour
{

    public GameObject spawnPrefab;
    private GameManager gameManager;
    private WaveSpawner waveSpawner;


    // Use this for initialization
    void Start()
    {
        //savedTime = Time.time;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        waveSpawner = gameObject.GetComponent<WaveSpawner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        MakeWave();

    }
    void MakeWave()
    {
        if (waveSpawner.doWaveSpawn())
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
            clone.gameObject.GetComponent<Chaser>().SetTarget(waveSpawner.GetWaveTarget());
            clone.transform.parent = this.transform;
            
        }
    }
}
