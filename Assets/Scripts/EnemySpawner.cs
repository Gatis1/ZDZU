using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private float spawnTime = 1.0f;
    [SerializeField] private int enemyNum = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnTime, zombie));
    }

    private IEnumerator spawnEnemy(float spawnTime, GameObject zombie)
    {
        for(int i = 0; i < enemyNum; i++)
        {
            yield return new WaitForSeconds(spawnTime);
            GameObject newZombie = Instantiate(zombie, new Vector3(Random.Range(-5f, 5f), 1, Random.Range(-5f,5f)), Quaternion.identity);
        }
    }
}
