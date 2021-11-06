using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    private GameObject player;

    [Header("Enemies")]
    public int numberOfEnemies;
    public bool multiplier = true;
    [Range(1, 2)]
    public float multiplierSpawn;

    [Header("Timers")]
    public bool spawnAcceleration = true;
    [Range(0.5f, 5)]
    public float timeBetTwoSpawn;
    [Range(1, 2)]
    public float timeSpawnAcceleration;
    [Range(0, 2)]
    public float variationTimeSpawn;

    [Header("GameManager")]
    public int round;
    [Range(1, 15)]
    public float timeBetweenRounds;


    private float currentTime;
    private float tempTime;

    private int currentNumberEnemies;

    private bool startRound;

    private bool isCopycat;

    // Start is called before the first frame update
    void Start()
    {
        currentNumberEnemies = numberOfEnemies;
        currentTime = timeBetTwoSpawn;
        startRound = true;
        round = 1;

        player = GameObject.Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).position = new Vector3(player.transform.position.x - 10, -2, 0);
        transform.GetChild(1).position = new Vector3(player.transform.position.x + 10, -2, 0);

        // Spawn amount of enemies define in GM
        if (currentNumberEnemies > 0 && startRound)
        {
            tempTime = Random.Range(timeBetTwoSpawn - variationTimeSpawn, timeBetTwoSpawn + variationTimeSpawn);

            if (currentTime >= tempTime)
            {
                if (round >= 4)
                {
                    int copycat = Random.Range(0, Mathf.RoundToInt(round * 1.7f));

                    if (copycat == 1)
                    {
                        isCopycat = true;
                    }
                }

                int temp = Random.Range(0, 2);

                if (temp == 0)
                {
                    Instantiate(enemy, transform.GetChild(0));
                    
                    if(isCopycat)
                    {
                        Instantiate(enemy, transform.GetChild(0));
                        isCopycat = false;
                    }
                }

                else if (temp == 1)
                {
                    Instantiate(enemy, transform.GetChild(1));

                    if (isCopycat)
                    {
                        Instantiate(enemy, transform.GetChild(1));
                        isCopycat = false;
                    }
                }

                currentTime = 0f;
                currentNumberEnemies -= 1;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }

        // Go to the next round
        if (currentNumberEnemies == 0)
        {
            round += 1;
            startRound = false;

            // if we want a multiplier
            if(multiplier)
            {
                numberOfEnemies = Mathf.RoundToInt(numberOfEnemies * multiplierSpawn);
            }

            // if we want a acceleration of spawning
            if(spawnAcceleration)
            {
                timeBetTwoSpawn /= timeSpawnAcceleration;

                if(timeBetTwoSpawn <= 0.55f)
                {
                    timeBetTwoSpawn = 0.55f;
                    spawnAcceleration = false;
                }
            }

            if(variationTimeSpawn <= 0.25f)
            {
                variationTimeSpawn = 0.25f;
            }
            else
            {
                variationTimeSpawn /= timeSpawnAcceleration;
            }

            currentNumberEnemies = numberOfEnemies;

            StartCoroutine(WaitSeconds(timeBetweenRounds));
        }
    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        startRound = true;
    }
}
