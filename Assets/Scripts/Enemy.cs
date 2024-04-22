using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    [SerializeField] private float mvSpd = 1.0f;
    [SerializeField] float healthValue, attackValue;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        CheckHealth();
    }

    // Update is called once per frame
    void Update()
    {
        swarm();
    }

    private void swarm(){transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mvSpd * Time.deltaTime);}

    public void UpdateHealth()
    {
        switch (this.gameObject.name)
        {
            case "computer science":
                healthValue = 3.0f;
                break;
            case "business":
                healthValue = 5.0f;
                break;
            case "art":
                healthValue = 10.0f;
                break;
            case "music theory":
                healthValue = 7.0f;
                break;
        }
    }

    public bool CheckHealth()
    {
        bool healthState = false;
        if(healthValue <= 0.0f)
        {
            healthState = false;
        }
        return healthState;
    }

    private float CheckAttack(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            /*
             * Speed values for each projectile would be good, this gives an advantage and disadvantage to each weapon/ball.
             */
            case "baseball":
                attackValue = 1.0f;
                break;
            case "volleyball":
                attackValue = 3.0f;
                break;
            case "football":
                attackValue = 6.0f;
                break;
        }
        return attackValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ball")
        {
            CheckAttack(collision);
        }
        if(!CheckHealth())
        {
            Destroy(this.gameObject);
        }
    }
}
