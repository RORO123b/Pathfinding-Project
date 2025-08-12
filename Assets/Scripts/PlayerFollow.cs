using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] Transform Player;

    Vector3 offset;

    void Start()
    {
        offset = new Vector3(0,2,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newpos = Player.position + offset;
        transform.position = newpos;
    }
}
