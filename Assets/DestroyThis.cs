using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GOT HIT");
        Destroy(gameObject);
    }
}
