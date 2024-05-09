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
    
    public bool CanHit = true;
    public float CoolDown = 1.5f;

    private EnemySpawner enemyCount;


    public enum TypeOfZombie
    {
        CS,
        Business,
        Art,
        Music
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        //CheckHealth();
        
        // Give appropriate health based on the zombie type
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(!CheckHealth())
        {
            Destroy(this.gameObject);
            enemyCount.EnemyKilled();

        }

        if (!CanHit)
        {
            // Start cooldown time
            StartCoroutine(Cooldown());
        }
        else
        {
            swarm(); 
        }
    }

    private void swarm(){transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mvSpd * Time.deltaTime);}

    public void UpdateHealth()
    {
        enemyCount.transform.gameObject.GetComponent<EnemySpawner>();
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
        }
    }
}
