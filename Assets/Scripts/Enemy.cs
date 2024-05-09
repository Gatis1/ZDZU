using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    [SerializeField] private float mvSpd = 1.0f;
    [SerializeField] public float healthValue, attackValue;
    public bool healthState;

    public TypeOfZombie type;
    public float physicalATK;
    public float mentalATK;

    public EnemyState action;
    
    public bool CanHit;
    public float CoolDown = 1.5f;
    private float distance;

    private EnemySpawner enemyCount;


    public enum TypeOfZombie
    {
        CS,
        Business,
        Art,
        Music
    }

    public enum EnemyState : int
    {
        chase = 0,
        Melee = 1,
        Shoot = 2,
        Buff = 3,
        stop
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        //CheckHealth();
        
        // Give appropriate stats based on the zombie type
        UpdateStats();

        //Can you hit deez nutz
        CanHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
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

        //Added if statements to check for the types of zombies and what actions they should do
        //Business is melee head to player and attack once they reach a certian distance.
        if(type == TypeOfZombie.Business)
        {
            if(distance > 1.5f)
            {
                action = EnemyState.chase;
            }
            else
            {
                action = EnemyState.Melee;
            }
        }
        //CS is a buffing character should append a one of two states (debating) an invincibility state to other zombies that are not CS
        //or a state where they do not take dmg until getting hit once.
        else if(type == TypeOfZombie.CS) {action = EnemyState.Buff;}
        //Music is a range character that once they spawn shoots at the player
        else if(type == TypeOfZombie.Music) {action = EnemyState.Shoot;}
        //Other types just do nothing and act as free points for the player to get
        else {action = EnemyState.stop;}

        switch(action)
        {
            case EnemyState.chase:
            swarm();
            break;

            case EnemyState.Melee:
            if (CanHit)
            { 
                meleeAttack();
            }
            break;

            case EnemyState.Shoot:
            rangeAttack();
            break;

            case EnemyState.Buff:
            buffZombies();
            break;

            case EnemyState.stop:
            break;
        }
    }

    private void swarm()
    {
        if(distance > 1.5f) {transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mvSpd * Time.deltaTime);}
    }

    private void meleeAttack()
    {
        if(CanHit == true)
        {
            player.physical -= physicalATK;
            player.mental -= mentalATK;
            CanHit = false;
        }
    }

    private void rangeAttack()
    {
        //The enemy shoot should instantiate an enemy bullet with force towards the player 
    }

    private void buffZombies()
    {
        //Check for other zombies in the scene and append a status to them.
        //Should change the spawner script so that only one CS spawns and cannot spawn another until the other dies.
    }

    public void UpdateStats()
    {
        //enemyCount.transform.gameObject.GetComponent<EnemySpawner>();
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
        healthState = true;
        if(healthValue <= 0.0f)
        {
            healthState = false;
        }
        return healthState;
    }

    private float CheckAttack(Collider collision)
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

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "ball")
        {
            CheckAttack(collision);
            healthValue -= attackValue;
        }
    }
}
