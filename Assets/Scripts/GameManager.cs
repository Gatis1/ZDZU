using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemySpawner zombies;
    [SerializeField] public int currentScore, enemyLevelCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        SceneTransition();
    }

    public void Score()
    { 
        currentScore += (2 * zombies.EnemyCount());
    }

    public void SceneTransition()
    {
        if (zombies.EnemyCount() == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        Score();
    }
}
