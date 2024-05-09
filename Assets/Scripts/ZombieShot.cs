using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieShot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") 
        {
            other.GetComponent<Player>().physical -= .2f;
            other.GetComponent<Player>().mental -= .2f;
            Destroy(gameObject);
        }
    }
}
