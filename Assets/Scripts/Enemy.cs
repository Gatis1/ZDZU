using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;

    //Zombie stats.
    [SerializeField] private float mvSpd = 1.0f;
    public float healthValue, attackValue;

    //Shooting variables
    [SerializeField] private float _shotInterval = 2.5f;
    [SerializeField] private float _shotSPD = 50f;
    [SerializeField] private float _meleeInterval = 4.0f;
    public GameObject _zombieShot;
    public Transform _shootPoint;
    public bool healthState;

    //Type and stats
    public TypeOfZombie type;
    public float physicalATK;
    public float mentalATK;
    private float _shotTime;
    private float _meleeTime;

    public EnemyState action;
    public float CoolDown = 1.5f;
    private float distance;
    private EnemySpawner enemyCount;
    
    // Models
    public GameObject BusinessModel;
    public GameObject MusicModel;


    //enum for the types of zombie enemies
    public enum TypeOfZombie : int
    { 
        Business = 0,
        Music = 1
    }

    //Different action states for each zombie
    public enum EnemyState
    {
        chase,
        Melee,
        Shoot,
        Buff,
        stop
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        //CheckHealth();
        
        // Give appropriate stats based on the zombie type
        UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        //get the distance between the player and enemies for attacking logic.
        distance = Vector3.Distance(transform.position, player.transform.position);
    
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
        // else if(type == TypeOfZombie.CS) {action = EnemyState.Buff;}
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
            meleeAttack();
            break;

            case EnemyState.Shoot:
            rangeAttack();
            break;

            case EnemyState.Buff:
            break;

            case EnemyState.stop:
            break;
        }
    }

    //movement function to go towards the player.
    private void swarm()
    {
        if(distance > 1.5f) {transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mvSpd * Time.deltaTime);}
    }

    //melee function to damage the playe when in range.
    private void meleeAttack()
    {
        _meleeTime -= Time.deltaTime;

        if(_meleeTime > 0) {return;}

        _meleeTime = _meleeInterval;

        player.physical -= physicalATK;
        player.mental -= mentalATK;
    }

    //shooting function to shot at the player.
    private void rangeAttack()
    {
        _shotTime -= Time.deltaTime;

        if(_shotTime > 0) { return; }

        _shotTime = _shotInterval;

        GameObject enemShot = Instantiate(_zombieShot, _shootPoint.transform.position, Quaternion.identity) as GameObject;
        Rigidbody eShotRig = enemShot.GetComponent<Rigidbody>();

        // Calculate direction towards player
        Vector3 direction = (player.transform.position - _shootPoint.transform.position).normalized;

        // Add force in the direction of the player
        eShotRig.AddForce(direction * _shotSPD);
    }

    public void UpdateStats()
    {
        //enemyCount.transform.gameObject.GetComponent<EnemySpawner>();
        switch (type)
        {
            // case TypeOfZombie.CS:
            //     healthValue = 2.0f;
            //     break;
            case TypeOfZombie.Business:
                GameObject model1 = Instantiate(BusinessModel, transform);
                model1.transform.localPosition = new Vector3(0f, -1f, 0f);
                healthValue = 5.0f;
                physicalATK = 2.0f;
                mentalATK = 2.0f;
                break;
            // case TypeOfZombie.Art:
            //     healthValue = 1.0f;
            //     break;
            case TypeOfZombie.Music:
                GameObject model2 = Instantiate(MusicModel, transform);
                model2.transform.localPosition = new Vector3(0f, -1f, 0f);
                healthValue = 7.0f;
                physicalATK = 3.0f;
                mentalATK = 2.0f;
                break;
        }
    }

    public bool CheckHealth()
    {
        if(healthValue <= 0.0f)
        {
            healthState = false;
        }
        else
        {
            healthState = true;
        }
        return healthState;
    }

    private float CheckAttack(Collider collision)
    {
        switch(collision.gameObject.tag)
        {
            case "baseball":
                attackValue = 1.0f;
                break;
        }
        return attackValue;
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
