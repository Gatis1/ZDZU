using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("REFERENCES")] 
    public GameObject ball;
    public static bool ReadyToShootBall = false;

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
        
        
        Debug.Log(Mathf.MoveTowards(test, 10, 3));
        Debug.Log(Mathf.MoveTowards(test, 10, 3));
        Debug.Log(Mathf.MoveTowards(test, 10, 3));
        Debug.Log(Mathf.MoveTowards(test, 10, 3));
        Debug.Log(Mathf.MoveTowards(test, 10, 3));
        Debug.Log(Mathf.MoveTowards(test, 10, 3));
        Debug.Log(Mathf.MoveTowards(test, 10, 3));
    }
    
    public void Shoot()
    {
        // Start animation
        animator.SetBool("hit", true);
        
        // Shoot from the cursor
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
        if (ReadyToShootBall)
        {
            // If we have a target, move the ball towards the enemy
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, target.position, TimeForBallToReachEnemy);
            
            // if the ball has reached the enemy, 
            if (ball.transform.position == target.position)
            {
                target = null;
                ReadyToShootBall = false;
            }
        }
    }
}
