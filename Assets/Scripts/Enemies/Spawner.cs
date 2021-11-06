using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    [Header("Settings")]
    public int numberOfEnemies;
    
    [Range(0,1)]
    public float leftOrRight;

    [Space(10)]

    public bool spawnAcceleration = true;
    [Range(0.5f, 3)]
    public float timeBetTwoSpawn;
    [Range(1, 2)]
    public float timeSpawnAcceleration;

    [Space(10)]

    public bool multiplier = true;
    [Range(1, 2)]
    public float multiplierSpawn;

    [Header("GameManager")]
    public int round;
    [Range(1, 15)]
    public float timeBetweenRounds;


    private float currentTime;

    private int currentNumberEnemies;
    private int EnemiesLeft;
    private int EnemiesRight;

    private bool isSwitch;
    private bool startRound;

    // Start is called before the first frame update
    void Start()
    {
        currentNumberEnemies = numberOfEnemies;
        currentTime = timeBetTwoSpawn;
        EnemiesRight = Mathf.RoundToInt(numberOfEnemies * leftOrRight);
        EnemiesLeft = numberOfEnemies - EnemiesRight;
        isSwitch = false;
        startRound = true;
        round = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn amount of enemies define in GM
        if (currentNumberEnemies > 0 && startRound)
        {
            if (currentTime >= timeBetTwoSpawn)
            {
                if(EnemiesLeft > 0 && isSwitch == false)
                {
                    Instantiate(enemy, transform.GetChild(0));
                    EnemiesLeft -= 1;
                    
                    if(EnemiesRight > 0)
                    {
                        isSwitch = true;
                    }
                }

                else if (EnemiesRight > 0 && isSwitch == true)
                {
                    Instantiate(enemy, transform.GetChild(1));
                    EnemiesRight -= 1;

                    if (EnemiesLeft > 0)
                    {
                        isSwitch = false;
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
            }

            currentNumberEnemies = numberOfEnemies;
            EnemiesRight = Mathf.RoundToInt(numberOfEnemies * leftOrRight);
            EnemiesLeft = numberOfEnemies - EnemiesRight;

            StartCoroutine(WaitSeconds(timeBetweenRounds));
        }
    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        startRound = true;
    }
}
