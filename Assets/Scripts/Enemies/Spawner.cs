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
    [Range(0.1f, 3)]
    public float timeBetTwoSpawn;

    [Space(10)]

    public bool multiplier = true;
    [Range(1, 2)]
    public float multiplierSpawn;

    [Space(20)]
    [Header("GameManager")]
    public int round;


    private float currentTime;
    private int currentNumberEnemies;

    // Start is called before the first frame update
    void Start()
    {
        currentNumberEnemies = numberOfEnemies;
        currentTime = timeBetTwoSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn amount of enemies define in GM
        if (currentNumberEnemies > 0)
        {
            if (currentTime >= timeBetTwoSpawn)
            {
                Instantiate(enemy, transform);
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
            numberOfEnemies = Mathf.RoundToInt(numberOfEnemies * multiplierSpawn);
            currentNumberEnemies = numberOfEnemies;
        }
    }
}
