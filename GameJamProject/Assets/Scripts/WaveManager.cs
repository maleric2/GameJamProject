using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager wm;
    public float secondsBetweenSpawning = 3f;
    public int maxNumberOfWaves;
    //public List<int> waveTargetNumber;
    public int currentTarget;

    public int currentWave = 1;

    public int startWaveNumber = 10;
    public int waveIncrement = 5;

    public int currentWaveSpawner = 0;
    public GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        if(wm == null)
            wm = gameObject.GetComponent<WaveManager>();

        gameManager = GameObject.FindObjectOfType<GameManager>();
        //waveTargetNumber = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Transform GetWaveTarget()
    {
        if (currentWave == 0) currentWave = 1;
        /*if (waveTargetNumber.Count < (currentWave)) //if there is new wave
        {
            GetRandomTarget();
            currentWave = waveTargetNumber.Count;
        }*/
        if (gameManager.targets[currentTarget] != null)
            return gameManager.targets[currentTarget];
        else
        {
            GetRandomTarget();
            return gameManager.targets[currentTarget];
        }
    }

   /* private void GetRandomTarget(int index)
    {
        waveTargetNumber.Insert(index, SetRandomTarget());
    }*/
    private void GetRandomTarget()
    {
        currentTarget = SetRandomTarget();
    }
    private int SetRandomTarget()
    {
        bool defined = false;
        int targetNumber;
        do
        {
            defined = true;
            targetNumber = Random.Range(0, gameManager.targets.Count);

            if (GameManager.gm.targets.Count > 0)
            {
                if (GameManager.gm.targets[targetNumber] == null) defined = false;
            }
            else Debug.Log("No targets");

        } while (!defined);

        Debug.Log("New Wave Target defined");
        return targetNumber;
    }
    public void makeWave(int wave)
    {
        if (wave > currentWave)
        {
            startWaveNumber += waveIncrement;
            currentWave = wave;
            Debug.Log("Get ready for wave " + currentWave);
            GetRandomTarget();
        }
    }

}
