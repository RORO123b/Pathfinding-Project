using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Transform tr;
    Collider2D _coll;
    public Transform orientation;
    public float nextWaypointDistance = 3f;
    private float lastJumpTime = -Mathf.Infinity;
    [SerializeField] private float jumpCooldown = 1f;

    float lastPhaseTime = -Mathf.Infinity;
    bool onGround = false;

    Path path;
    int currentWaypoint = 0;
    bool reached = false;

    // bool platformCollided = false;
    // bool playerCollided = false;

    public Transform target;
    Seeker seeker;
    [SerializeField] float speed = 10f, jump = 10f;

    // public Collider2D Platform;
    // public Collider2D Platform1;
    // public Collider2D Platform2;
    // public Collider2D playercoll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        _coll = GetComponent<Collider2D>();
        seeker = GetComponent<Seeker>();
        target = GameObject.Find("Player").transform;

        // OLD

        // Platform = GameObject.Find("Platform").GetComponent<Collider2D>();
        // Platform1 = GameObject.Find("Platform1").GetComponent<Collider2D>();
        // Platform2 = GameObject.Find("Platform2").GetComponent<Collider2D>();
        // playercoll = GameObject.Find("Player").GetComponent<Collider2D>();
    }

    private void Start()
    {
        // Physics2D.IgnoreCollision(playercoll, _coll, true);
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    /// Brute force pathfinding -- OLD

    // void Update()
    // {
    //     if ((float)Player.position.x > (float)tr.position.x)
    //         rb.velocity = new Vector2(speed, rb.velocity.y);
    //     if ((float)Player.position.x < (float)tr.position.x)    
    //         rb.velocity = new Vector2(-speed, rb.velocity.y);
    //     if (playerCollided == true)
    //         rb.velocity = new Vector2(0, rb.velocity.y);
    //     if ((float)Player.position.y > (float)tr.position.y + 2 && PlayerIsNear() && platformCollided==true)
    //     {
    //         rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    //         platformCollided = false;
    //     }
    //     if ((float)Player.position.y + 1 < (float)tr.position.y)
    //     {
    //         Physics2D.IgnoreCollision(Platform, _coll, true);
    //         Physics2D.IgnoreCollision(Platform1, _coll, true);
    //         Physics2D.IgnoreCollision(Platform2, _coll, true);
    //         Invoke("CollisionBackToNormal", 0.5f);
    //     }
    // }


    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reached = true;
            return;
        }
        else
        {
            reached = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        float force = Mathf.Sign(direction.x) * speed * Time.deltaTime;

        rb.velocity = new Vector2(force, rb.velocity.y);

        // Jump
        if (direction.y >= 0.8f && Time.time - lastJumpTime > jumpCooldown && onGround == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            lastJumpTime = Time.time;
        }


        // Phase through platforms
        //if (direction.y < -0.8f && Time.time - lastPhaseTime > 1f)
        //{

        //    RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 3f, LayerMask.GetMask("Platform"));
        //    if (hit.collider != null)
        //    {
        //        StartCoroutine(PhaseThroughPlatform(hit.transform.GetComponent<Collider2D>()));
        //        lastPhaseTime = Time.time;
        //    }
        //}

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;

        if (rb.velocity.x >= 0.01f)
            orientation.localScale = new Vector3(-1f, 1f, 1f);
        else if (rb.velocity.x <= -0.01f)
            orientation.localScale = new Vector3(1f, 1f, 1f);
    }


    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            onGround = true;
        }
    }


    //IEnumerator PhaseThroughPlatform(Collider2D platformCollider)
    //{
    //    PlatformEffector2D effector = platformCollider.GetComponent<PlatformEffector2D>();
    //    if (effector != null)
    //        effector.rotationalOffset = 180f;

    //    yield return new WaitUntil(() =>
    //        rb.position.y + _coll.bounds.extents.y < platformCollider.bounds.min.y);

    //    yield return new WaitForSeconds(0.2f);
    //    if (effector != null)
    //        effector.rotationalOffset = 0f;
    //}






    // private bool PlayerIsNear()
    // {
    //     if (Mathf.Abs((float)Player.position.x - (float)tr.position.x) < 4f)
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Platform")
    //         platformCollided = true;
    //     if(collision.gameObject.tag == "Enemy")
    //         Physics2D.IgnoreCollision(collision.collider, _coll, true);
    // }
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //         playerCollided = true;
    // }
    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     playerCollided = false;
    // }

}
