using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D rb;

    bool onGround = false;

    [SerializeField]
    float jump = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            onGround = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround==true)
        {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            onGround = false;
        }
    }
}
