using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Button shootingButton;
    public Image cursor;
    public LayerMask EnemyLayerMask;

    void Start()
    {
        // Add listener to the button's onClick event
        shootingButton.onClick.AddListener(Shoot);
        
    }


    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);
        RaycastHit hit;
        float infinity = Mathf.Infinity;
        
        if (Physics.Raycast(ray, out hit, infinity, EnemyLayerMask))
        {
            Debug.Log(hit.transform.gameObject.name + " takes X amount of DAMAGE");
            
        }
        
    }
}
