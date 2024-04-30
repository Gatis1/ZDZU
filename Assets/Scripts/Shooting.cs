using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("REFERENCES")] 
    public GameObject OGball; 
    public static bool ReadyToShootBall = false;
    public GameObject ball;
    public GameObject ballToShoot;

    [Header("TARGET")]
    public Transform target;
    public float TimeForBallToReachEnemy;
    
    [Header("UI AND ANIMATION")]
    public Button shootingButton;
    public Image cursor;
    public LayerMask EnemyLayerMask;
    public Animator animator;

    public float test = 0;
    
    void Start()
    {
        // Add listener to the button's onClick event
        shootingButton.onClick.AddListener(Shoot);
        target = null;
    }
    
    public void Shoot()
    {
        // Start animation
        animator.SetBool("hit", true);
        
        // Shoot RAY from the cursor to get data on enemy if there is an enemy
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);

        // if the cursor hits an enemy
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, EnemyLayerMask))
        {
            Debug.Log(hit.transform.gameObject.name + " takes X amount of DAMAGE");
            // Record the hit's transform
            target = hit.transform;
        }
    }

    // This method below depends on the animation script for baseball
    private void Update()
    {
        // If the animation ends start ball movement
        if (ReadyToShootBall && target != null)
        {
            Debug.Log(ReadyToShootBall);

            // Make a new ball. This ball will be the one to shoot
            if (ballToShoot == null)
            {
                ballToShoot = Instantiate(ball, OGball.transform.position, Quaternion.identity);
            }
            
            // If we have a target, move the ball towards the enemy
            ballToShoot.transform.position = Vector3.MoveTowards(ballToShoot.transform.position, target.position, TimeForBallToReachEnemy);
            
            // if the ball has reached the enemy, 
            if (ballToShoot.transform.position == target.position)
            {
                Debug.Log("Ball reached target");
                target = null;
                ReadyToShootBall = false;
                Destroy(ballToShoot);
            }
        }
    }
}
