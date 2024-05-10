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
        if(Health <= 0.0f)
        {
            /* Either a new scene where we present the player with the game over screen and message.
             * This could be a new scene, or a canvas pops up and covers the entire screen. This acts as as the game over screen without loading a new scene.
             */
            SceneManager.LoadScene("GameOver");
            
        }
    }
}
