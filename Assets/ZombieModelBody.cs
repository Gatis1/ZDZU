using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieModelBody : MonoBehaviour
{
    private void Update()
    {
        transform.localPosition = new Vector3(0f, -1f, 0f);
    }
}
