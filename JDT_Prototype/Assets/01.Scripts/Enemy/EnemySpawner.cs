using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public float spawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            int count = Random.Range(1, 6);

            for (int i = 0; i < count; i++)
            {
                float randomX = Random.Range(0, 3.5f);
                float randomY = Random.Range(0, 3.5f);

                Instantiate(enemy, transform.position + new Vector3(randomX, randomY), Quaternion.identity, transform);
            }
        }
    }
}
