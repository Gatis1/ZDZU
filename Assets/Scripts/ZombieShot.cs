using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieShot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") 
        {
            other.GetComponent<Player>().Health -= .2f;
            Destroy(gameObject);
        }
    }
}
