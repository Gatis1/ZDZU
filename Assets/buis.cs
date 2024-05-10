using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buis : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void die()
    {
        animator.SetBool("dying", true);
    }
    public void attack()
    {
        animator.SetBool("attackng", true);
    }
    public void stopAttacking()
    {
        animator.SetBool("attacking", false);
    }
}
