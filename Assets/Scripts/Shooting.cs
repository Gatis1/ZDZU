using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Button shootingButton;
    public Image cursor;
    public LayerMask EnemyLayerMask;
    public Animator animator;

    public bool test = false;

    void Start()
    {
        // Add listener to the button's onClick event
        shootingButton.onClick.AddListener(Shoot);
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
            // Do something
            Debug.Log(hit.transform.gameObject.name + " takes X amount of DAMAGE");
        }
    }

    private void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("baseball"))
        {
            // Animation has ended, perform your actions here
            test = true;
        }
        else
        {
            test = false;
        }
    }
}
