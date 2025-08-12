using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    StartLevelScript enemies;

    [SerializeField] Text NRHealth;
    [SerializeField] GameObject HPbox1, HPbox2, HPbox3, HPbox4, HPbox5;

    private float MAX_HEALTH = 100f;
    public float health = 100f;

    public int coinsneeded = 2;
    public int maxHPlevel = 5;
    public int currentHPlevel = 0;
    
    public Text HPText;
    public Text coinsneededforHP;

    public Coins c;

    private Image healthBar;

    private void Awake()
    {
        c = GameObject.FindObjectOfType<Coins>();
        enemies = GameObject.FindObjectOfType<StartLevelScript>();
    }

    void Start()
    {
        healthBar = GetComponent<Image>();
    }
    void Update()
    {
        if (currentHPlevel < maxHPlevel)
            coinsneededforHP.text = coinsneeded.ToString();
        else
            coinsneededforHP.text = "Max Level";

        healthBar.fillAmount=health / MAX_HEALTH;
        NRHealth.text = health.ToString();

        if(health == 0)
            Time.timeScale = 0;

        if (enemies.currenemies == 0)
            health = MAX_HEALTH;
    }

    public void HealthBuy()
    {
        if (coinsneeded <= c.coins && currentHPlevel < maxHPlevel)
        {
            currentHPlevel++;
            if (currentHPlevel == 1)
            {
                HPbox1.GetComponent<Image>().color = Color.red;
            }
            else if (currentHPlevel == 2)
            {
                HPbox2.GetComponent<Image>().color = Color.red;
            }
            else if (currentHPlevel == 3)
            {
                HPbox3.GetComponent<Image>().color = Color.red;
            }
            else if (currentHPlevel == 4)
            {
                HPbox4.GetComponent<Image>().color = Color.red;
            }
            else
                HPbox5.GetComponent<Image>().color = Color.red;
            c.coins -= coinsneeded;
            coinsneeded += 2;
            if (currentHPlevel % 2 == 0 || currentHPlevel == 5)
                MAX_HEALTH += 100;
            else
                MAX_HEALTH += 50;
        }
    }
}
