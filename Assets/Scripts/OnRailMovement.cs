using UnityEngine;

public class OnRailMovement : MonoBehaviour
{
    [Header("MOVEMENT VARS")]
    public float speed;
    
    [Header("OBJECT FOR POSITIONS")]
    public GameObject target;
    
    void Update()
    {
        if (transform.position != target.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        }
    }
}
