using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float min_Y = -4.3f, max_Y = 4.3f;

    public GameObject[] AsteroidPrefabs;

    public GameObject enemyPrefab;

    public float timer = 2f;

    void Start()
    {
        Invoke("SpawnEnemies", timer); // first call for the function (then, it will be "calling itself" by Invoke inside it)
    }

    void SpawnEnemies()
    {
        float pos_Y = Random.Range(min_Y, max_Y);
        Vector3 temp = transform.position;
        temp.y = pos_Y;

        if (Random.Range(0, 2) == 0) // create one asteroid 
        {

            Instantiate(AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length)], temp, Quaternion.identity);
        }
        else // random == 1 => create an enemy
        {
            Instantiate(enemyPrefab, temp, Quaternion.Euler(0f, 0f, 90f));
        }
        Invoke("SpawnEnemies", timer); // calling this function over and over again, in each 2f seconds
    }

}
