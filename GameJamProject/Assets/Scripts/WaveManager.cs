using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager wm;
    public int registeredSpawners = 0;
    public int spawnersCompletedWave = 0;

    public float secondsBetweenWaves = 20.0f;
    public float secondsBetweenSpawning = 3f;
    public int maxNumberOfWaves;
    //public List<int> waveTargetNumber;
    public int currentTarget = -1;

    public int currentWave = 1;

    public int startWaveNumber = 10;
    public int waveIncrement = 5;

    public int currentWaveSpawner = 0;
    public GameManager gameManager;
    public bool newWaveGenerated = false;
    // Use this for initialization
    void Start()
    {
        if (wm == null)
            wm = gameObject.GetComponent<WaveManager>();

        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Transform GetWaveTarget()
    {
        /*Debug.Log("GetWaveTarget");

        if (currentTarget < 0)
            GetRandomTarget();
        if (currentWave == 0) currentWave = 1;

        if (gameManager.targets.Count > currentTarget && gameManager.targets[currentTarget] != null) { 
            
            if(gameManager.targets[currentTarget].GetComponent<HealthController>().isFullHealth())
                GetRandomTarget();
            return gameManager.targets[currentTarget];
        }
        else
        {
            GetRandomTarget();
            return gameManager.targets[currentTarget];
        }*/
        return gameManager.targets[currentTarget];
    }

    /* private void GetRandomTarget(int index)
     {
         waveTargetNumber.Insert(index, SetRandomTarget());
     }*/
    public void GetRandomTarget()
    {
        if(!newWaveGenerated)
            currentTarget = SetRandomTarget();

    }
    private int SetRandomTarget()
    {
        bool defined = false;
        int targetNumber;
        do
        {
            defined = false;
            targetNumber = Random.Range(0, gameManager.targets.Count);
            Debug.Log("Random: " + targetNumber);

            if (GameManager.gm.targets.Count > 0)
            {
                if (GameManager.gm.targets.Count > targetNumber && GameManager.gm.targets[targetNumber] != null)
                {
                    defined = true;
                    Debug.Log("Target Good");
                }
            }
            else Debug.Log("No targets (ERROR)");

        } while (!defined);
        newWaveGenerated = true;
        Debug.Log("New Wave Target defined");
        return targetNumber;
    }
    public void makeWave(int wave)
    {

        if (wave > currentWave)
        {
            newWaveGenerated = false;
            GetRandomTarget();
            startWaveNumber += waveIncrement;
            spawnersCompletedWave = 0;
            currentWave = wave;
            Debug.Log("Get ready for wave " + currentWave);
        }
    }

    public void RegisterSpawner()
    {
        registeredSpawners++;
    }
    public void ICompletedWave()
    {
        spawnersCompletedWave++;
    }
    public bool SpawnersCompletedWave()
    {
        if (registeredSpawners <= spawnersCompletedWave) return true;
        else return false;
    }
}
