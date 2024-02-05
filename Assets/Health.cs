using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static GlobalController;
using static CharacterBase;
using TMPro;


public class Health : MonoBehaviour
{
    public TMPro.TextMeshProUGUI healthText;
    public Image healthBar;
    float health, maxHealth = 70;
    float lerpSpeed;

/*
    public int redstickmanhealth;
    public int bluestickmanhealth;

    public GlobalController game;

    public GameObject[] maps;
    public GameObject map;
    public GameObject[] characters;

*/
    void Start()
    {
        health = maxHealth;
        healthText.text = "Health: 100";
        Debug.Log("health Start");
        healthBar.fillAmount = health;
        
        lerpSpeed = 1f;
        HealthBarFiller();
        ColorChanger();
    }

    void Update()
    {
        if (healthText != null)
        {
            Debug.Log(healthText.text);
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found or assigned.");
        }
        healthText.text = "Health: " + health.ToString() + "%";

        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        //Debug.Log("healthBar = " + healthBar);
        //Debug.Log("healthBar.fillAmount = " + healthBar.fillAmount);


        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);


    }
    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
        
    }

    /**bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }*/

    public void Damage(float damagePoints)
    {
        Debug.Log("Damage is working");
        if (health > 0)
            health -= damagePoints;

    }
    public void Heal(float healingPoints)
    {
        Debug.Log("Heal is working");
        if (health < maxHealth)
            health += healingPoints;
     
    }
}
