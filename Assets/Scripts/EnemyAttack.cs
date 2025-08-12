using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public PlayerHealth playerHP;

    bool playerCollided = false;

    public float attackcooldown = 1f;
    float lastslash = -100f;

    Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastslash + (1f / attackcooldown))
        {
            lastslash = Time.time;
            if (playerCollided == true)
            {
                playerHP.health -= 10;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            coll = collision;
            playerCollided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        coll = null;
        playerCollided = false;
    }
}
