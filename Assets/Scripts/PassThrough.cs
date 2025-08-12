using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThrough : MonoBehaviour
{
    private Collider2D _coll;

    [SerializeField] Transform Player;

    private void Awake()
    {
        _coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), _coll,true);
            Invoke("CollisionBackToNormal", 0.5f);
        }
    }
    void CollisionBackToNormal()
    {
        Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), _coll,false);
    }
}
