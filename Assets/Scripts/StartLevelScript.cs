using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelScript : MonoBehaviour
{
    public int enemies = 0, currenemies;
    public int flyingEnemies = 0;

    int lastspawn = 0;

    public GameObject enemy;
    public GameObject flyingEnemy;
    public GameObject StartLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartLevel()
    {
        if(PauseMenuMainGameScript.GameIsPaused == false)
        {
            enemies++;
            if (enemies % 2 == 0)
            {
                flyingEnemies++;
            }
            currenemies = enemies + flyingEnemies;

            StartLevelButton.SetActive(false);
            StartCoroutine(SpawnCooldown());
        }
    }

    IEnumerator SpawnCooldown()
    {
        int spawned = enemies;
        for (int i = 0; i < currenemies; i++)
        {
            if (spawned > 0)
            {
                spawned--;
                int rand = lastspawn;
                while (rand == lastspawn)
                    rand = Random.Range(1, 4);
                lastspawn = rand;
                if (rand == 1)
                    Instantiate(enemy, new Vector2(-11f, -2f), Quaternion.identity);
                else if (rand == 2)
                    Instantiate(enemy, new Vector2(11f, -2f), Quaternion.identity);
                else if (rand == 3)
                    Instantiate(enemy, new Vector2(-6, 6), Quaternion.identity);
                else
                    Instantiate(enemy, new Vector2(6, 6), Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                int rand = lastspawn;
                while (rand == lastspawn)
                    rand = Random.Range(3, 4);
                lastspawn = rand;
                if (rand == 3)
                    Instantiate(flyingEnemy, new Vector2(-6, 6), Quaternion.identity);
                else if (rand == 4)
                    Instantiate(flyingEnemy, new Vector2(6, 6), Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
