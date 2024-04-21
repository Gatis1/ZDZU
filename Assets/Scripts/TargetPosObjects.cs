using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

[RequireComponent(typeof(BoxCollider))]
public class TargetPosObjects : MonoBehaviour
{
    public GameObject nextTarget;
    public WhatToDoWhenTouched whatToDo;

    // Different desired actions once player reaches this target postion
    public enum WhatToDoWhenTouched
    {
        Stay,
        GoToNextTarget
    }

    public void OnTriggerStay(Collider other)
    {
        // If player touches this object, and we want to move to the next target, then update the new target for player
        if (other.gameObject.tag == "Player" && whatToDo == WhatToDoWhenTouched.GoToNextTarget)
        {
            /* The reason for this being the parent is because the body of the player
             * which is a child object of the player (which holds the collider component)
             * is what triggers this
             */
            other.GetComponentInParent<OnRailMovement>().target = nextTarget;
        }    
    }
}
