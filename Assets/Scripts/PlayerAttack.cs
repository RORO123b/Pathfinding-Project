using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject ASbox1, ASbox2, ASbox3, ASbox4, ASbox5;
    bool enemyCollided = false;
    public float attackcooldown = 5f;
    public GameObject StartLevelButton;
    public int coinsneeded = 2;
    public int maxASlevel = 5;
    public int currentASlevel = 0;

    public Text ASText;
    public Text coinsneededforAS;

    public StartLevelScript currentenemies;

    public Coins c;

    Collider2D coll;

    float lastslash = -100f;

    private void Awake()
    {
        currentenemies=GameObject.FindObjectOfType<StartLevelScript>();
        c = GameObject.FindObjectOfType<Coins>();
    }
    private void Update()
    {
        if (currentASlevel < maxASlevel)
            coinsneededforAS.text = coinsneeded.ToString();
        else
            coinsneededforAS.text = "Max Level";
        float cv = lastslash + (1f / attackcooldown) - Time.time;
        if (cv <= 0)
            ASText.text = "";
        else
            ASText.text = cv.ToString("F2");
        if(Input.GetMouseButton(0) && Time.time>=lastslash+(1f/attackcooldown))
        {
            lastslash = Time.time;
            if (enemyCollided == true)
            {
                c.coins++;
                currentenemies.currenemies--;
                if (currentenemies.currenemies == 0)
                    StartLevelButton.SetActive(true);
                Destroy(coll.gameObject);
                enemyCollided = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            coll = collision;
            enemyCollided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            coll = null;
            enemyCollided = false;
    }
    public void AttackSpeedBuy()
    {
        if(coinsneeded<=c.coins && currentASlevel < maxASlevel)
        {
            currentASlevel++;
            if(currentASlevel==1)
            {
                ASbox1.GetComponent<Image>().color = Color.red;
            }
            else if(currentASlevel==2)
            {
                ASbox2.GetComponent<Image>().color = Color.red;
            }
            else if (currentASlevel == 3)
            {
                ASbox3.GetComponent<Image>().color = Color.red;
            }
            else if (currentASlevel == 4)
            {
                ASbox4.GetComponent<Image>().color = Color.red;
            }
            else
                ASbox5.GetComponent<Image>().color = Color.red;
            c.coins -= coinsneeded;
            coinsneeded += 2;
            attackcooldown++;
        }
    }
}
