# Unity 2D Pathfinding Project

This project implements **2D pathfinding** in Unity using the [A\* Pathfinding Project](https://arongranberg.com/astar) package.\
It includes a custom **graph creation tool** for grounded movement, allowing you to manually design navigable areas and tweak pathfinding behavior.

## Features

- **A**\*-based pathfinding for 2D environments
- Custom **graph creation tool** for grounded movement
- Gizmo visualization for debugging paths
- Adjustable movement parameters for agents

## Installation

1. Clone or download this repository.
2. Open the project in Unity.
3. Open the `Scenes` folder and run the demo scene.

## Usage

1. Use the **Graph Creation Tool** to define navigable nodes (found in the Tools tab).
2. Assign an agent prefab to the scene.
3. Assign the Player or anything you desire as the Target.
4. Press Play to test pathfinding between points.

## Options

- **Nodes per Unit**
- **Minimum nodes** (per platform)
- **Vertical offset**

## How it Works

### Graph Creation Tool

The custom Editor Window generates empty GameObjects over each platform (tagged `Platform`) and groups them under one parent for easy management. The generated nodes are connected using the A\* package to create a graph for pathfinding.

```c#
void GenerateNodes()
{
    GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
    if (platforms.Length == 0)
    {
        Debug.LogWarning("No GameObjects found with tag 'Platform'.");
        return;
    }

    GameObject parent = new GameObject("GeneratedNodes");

    foreach (GameObject platform in platforms)
    {
        Renderer renderer = platform.GetComponent<Renderer>();

        float platformWidth = renderer.bounds.size.x;
        int nodeCount = Mathf.Max(minNodes, Mathf.RoundToInt(platformWidth * nodesPerUnit));

        for (int i = 0; i < nodeCount; i++)
        {
            float t = (nodeCount == 1) ? 0.5f : (float)i / (nodeCount - 1);
            float xPos = renderer.bounds.min.x + t * platformWidth;
            float yPos = renderer.bounds.max.y + verticalOffset;

            GameObject Node = new GameObject($"Node_{platform.name}_{i}");
            Node.transform.position = new Vector3(xPos, yPos, platform.transform.position.z);
            Node.transform.SetParent(parent.transform);
        }
    }
}
```

### Flying Enemy Pathfinding

Uses a Grid Graph from the A\* package. The enemy follows waypoints along the shortest path and orients itself towards the next waypoint.

```c#
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
    Vector2 force = direction * speed * Time.deltaTime;

    rb.velocity = force;

    float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
    if (distance < nextWaypointDistance)
        currentWaypoint++;

    if (rb.velocity.x >= 0.01f)
        orientation.localScale = new Vector3(-1f, 1f, 1f);
    else if (rb.velocity.x <= -0.01f)
        orientation.localScale = new Vector3(1f, 1f, 1f);
}
```
![Flying Enemy Path](Images/Flying_Enemy_Path.png)

### Ground Enemy Pathfinding

Similar to flying enemies but adapted for grounded movement. The character will jump if the path requires moving to a higher platform.

```c#
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

    if (direction.y >= 0.8f && Time.time - lastJumpTime > jumpCooldown && onGround)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        lastJumpTime = Time.time;
    }

    float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
    if (distance < nextWaypointDistance)
        currentWaypoint++;

    if (rb.velocity.x >= 0.01f)
        orientation.localScale = new Vector3(-1f, 1f, 1f);
    else if (rb.velocity.x <= -0.01f)
        orientation.localScale = new Vector3(1f, 1f, 1f);
}
```
![Flying Enemy Path](Images/Ground_Enemy_Path.png)
## External Sources

- **Pathfinding:** [A\* Pathfinding Project](https://arongranberg.com/astar) by Aron Granberg

