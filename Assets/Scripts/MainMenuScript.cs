using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void TestGroundEnemy()
    {
        SceneManager.LoadScene("GroundEnemyTest");
    }

    public void TestFlyingEnemy()
    {
        SceneManager.LoadScene("FlyingEnemyTest");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
