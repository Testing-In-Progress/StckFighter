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
    public Image[] healthPoints;

    float health, maxHealth = 100;
    float lerpSpeed;

/*
    public int redstickmanhealth;
    public int bluestickmanhealth;

    public GlobalController game;

    public GameObject[] maps;
    public GameObject map;
    public GameObject[] characters;

*/
    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        healthText.text = "Health: " + health.ToString("0.00") + "%";

        Debug.Log(healthText.text);
        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();

        if (healthText == null) Debug.LogError("healthText is not assigned!");
        if (healthBar == null) Debug.LogError("healthBar is not assigned!");
        if (healthPoints == null) Debug.LogError("healthPoints array is not initialized!");


    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);

        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayHealthPoint(health, i);
        }
    }
    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
        
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
            health -= damagePoints;

    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
     
    }
}
