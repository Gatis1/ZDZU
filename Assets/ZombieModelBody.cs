using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieModelBody : MonoBehaviour
{
    // Reference to enemy script and animator
    public Enemy enemy;
    public Animator animator;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Keep model stuck to the zombie object
        transform.localPosition = new Vector3(0f, -1f, 0f);
        
        // Update Animation based on 
        GetDesiredAnimation();
    }

    public void GetDesiredAnimation()
    {
        // If it's music zombie
        if (enemy.type == Enemy.TypeOfZombie.Music)
        {
            // Make it stand still in idle, which is their entry state. So no need to do anything
            return;
        }

        // if it's business
        if (enemy.type == Enemy.TypeOfZombie.Business)
        {
            // Check the state and apply the desired animation
            switch (enemy.action)
            {
                case Enemy.EnemyState.chase:
                    animator.SetBool("attacking", false);
                    animator.SetBool("walking", true);
                    break;
                
                case Enemy.EnemyState.Melee:
                    animator.SetBool("walking", false);
                    animator.SetBool("attacking", true);
                    break;
                
            }
        }
    }
}
