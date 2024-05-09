using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private float spawnTime = 1.0f;
    [SerializeField] private int enemyNum = 5;
    [SerializeField] private float spawningRadiusAroundSpawner = 5f;

    // Number of enemies per level.
    [SerializeField] static int enemyCount;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnTime, zombie));
    }

    public int EnemyCount()
    {
        return enemyCount;
    }

    public int EnemyKilled()
    {
        return enemyCount--;
    }

    private IEnumerator spawnEnemy(float spawnTime, GameObject zombie)
    {
        for(int i = 0; i < enemyNum; i++)
        {
            // limits enemy spawning to the spawnTime interval
            yield return new WaitForSeconds(spawnTime);

            // spawn enemies
            GameObject newZombie = Instantiate(zombie, Vector3.zero, Quaternion.identity);
            // and make it a child of the spawner so that when we choose it's position, it's around the spawner
            newZombie.transform.parent = this.transform;
            newZombie.transform.localPosition = new Vector3(Random.Range(-spawningRadiusAroundSpawner, spawningRadiusAroundSpawner), 1, Random.Range(-spawningRadiusAroundSpawner, spawningRadiusAroundSpawner));
            //Make the enemies vary in what actions they do when they spawn either melee or shooting.
            Enemy enemyState = newZombie.GetComponent<Enemy>();
            enemyState.type = (Enemy.TypeOfZombie)Random.Range(0,3);

            enemyCount++;
        }
    }
}
