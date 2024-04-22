using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    [SerializeField] private float mvSpd = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
    }

    // Update is called once per frame
    void Update()
    {
        swarm();
    }

    private void swarm(){transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mvSpd * Time.deltaTime);}
}
