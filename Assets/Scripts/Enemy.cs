using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    [SerializeField] private float mvSpd = 1.0f;
    [SerializeField] float healthValue, attackValue;

    public TypeOfZombie type;
    public float physicalATK;
    public float mentalATK;

    public EnemyState action;
    
    public bool CanHit = true;
    public float CoolDown = 1.5f;
    private float distance;


    public enum TypeOfZombie
    {
        CS,
        Business,
        Art,
        Music
    }

    public enum EnemyState : int
    {
        Melee = 0,
        Shoot = 1,
        Buff = 2,
        Explode = 3,
        stop
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        CheckHealth();
        
        // Give appropriate stats based on the zombie type
        UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        //get the distance between the player and enemies for attacking logic.
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (!CanHit)
        {
            // Start cooldown time
            StartCoroutine(Cooldown());
        }
        
        if(!CheckHealth())
        {
            Destroy(this.gameObject);
        }

        switch(action)
        {
            case EnemyState.Melee:
            swarm();
            //do a melee attack
            break;

            case EnemyState.Shoot:
            swarm();
            //shoot at the player
            break;

            case EnemyState.Buff:
            swarm();
            //Do a buffing function
            break;

            case EnemyState.Explode:
            swarm();
            //run up to player 
            break;
        }
    }

    private void swarm()
    {
        //the enemies will stop at a certian distance from the player and perform their actions accordingly
        if ((action == EnemyState.Melee && distance >= 1.5f) || (action == EnemyState.Shoot && distance > 5.0f))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mvSpd * Time.deltaTime);
        }
    }

    public void UpdateStats()
    {
        switch (type)
        {
            case TypeOfZombie.CS:
                healthValue = 3.0f;
                physicalATK = 1.0f;
                mentalATK = 5.0f;
                break;
            case TypeOfZombie.Business:
                healthValue = 5.0f;
                physicalATK = 2.0f;
                mentalATK = 2.0f;
                break;
            case TypeOfZombie.Art:
                healthValue = 10.0f;
                physicalATK = 4.0f;
                mentalATK = 1.5f;
                break;
            case TypeOfZombie.Music:
                healthValue = 7.0f;
                physicalATK = 3.0f;
                mentalATK = 2.0f;
                break;
        }
    }

    public bool CheckHealth()
    {
        bool healthState = true;
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

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CoolDown);
        CanHit = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ball")
        {
            CheckAttack(collision);
            healthValue -= attackValue;
        }
    }
}
