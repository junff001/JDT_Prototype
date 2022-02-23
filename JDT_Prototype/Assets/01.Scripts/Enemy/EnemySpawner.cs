using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Transform> spawnTrms;

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

            for(int i = 0; i< spawnTrms.Count; i++)
            {
                int randMobIdx = Random.Range(0, enemies.Count);

                Instantiate(enemies[randMobIdx], spawnTrms[i].position, Quaternion.identity, transform);

            }
        }
    }
}
