using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public float Health = 20.0f;
    [SerializeField] Rigidbody playerBody;

    public HPbar HP;
    public float currHP;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        HP.setMaxHealth(Health);
    }

    private void Update() {
        currHP = Health;
        HP.setHealth(currHP);
    }

    /*
     * We can have one game over function that loads the game over scene, and additionally have three functions.
     * One for each of the different type of game over screens. This way everything can be modular and not have long functions and to have more flexibility.
     */
    public void GameOver()
    {
        /* How about having 2/3 different game over screens.
         * One for for losing condition, The player loses all physical health value, mental or both.
         * Each one has a different message, or even specific to the type of zombie that killed them.
         * Nothing big or major, just a different game over message.
         */
        if(Health <= 0.0f)
        {
            /* Either a new scene where we present the player with the game over screen and message.
             * This could be a new scene, or a canvas pops up and covers the entire screen. This acts as as the game over screen without loading a new scene.
             */
            SceneManager.LoadScene("GameOver");
            
        }
    }
}
