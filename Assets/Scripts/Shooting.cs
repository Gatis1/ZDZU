using System;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [Header("UI AND ANIMATION")]
    public Button shootingButton;
    public Image cursor;
    public LayerMask EnemyLayerMask;
    public Animator animator;
    
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
            // Do stuff!!!
            Debug.Log(hit.transform.gameObject.name + " takes X amount of DAMAGE");
            Destroy(hit.transform.gameObject);
        }
    }

}
