using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using Unity.VisualScripting;
using UnityEngine;

using static GlobalController;
using static CharacterBase;
using TMPro;

public class GameController : MonoBehaviour
{
    public GlobalController game;
    public int Andrehealth;
    public int FLLFFLhealth;
    public GameObject[] maps;
    public GameObject map;
    public GameObject healthPrefab;
    public GameObject[] characters;

    public Canvas canvas1;
    public Canvas canvas2;

    public GameObject Healthpreset1;
    public GameObject Healthpreset2;



  /*  public TMPro.TextMeshProUGUI healthText;
    public Image healthBar;*/
    float health, maxHealth = 100;
    float lerpSpeed;

    GameObject getMap(string mapName) {
        foreach (GameObject map in maps) {
            if (map.name == mapName) {
                return map;
            }
        }
        return new GameObject();
    }
    GameObject getCharacter(string characterName) {
        foreach (GameObject character in characters) {
            if (character.name == characterName) {
                return character;
            }
        }
        return new GameObject();
    }
    float getPosFromMap(int count, int index) {
        Debug.Log("Posing");
        float mapWidth = map.transform.Find("mainFloor").GetComponent<BoxCollider2D>().bounds.size.x;
        Debug.Log(mapWidth);
        float startingPoint = map.transform.position.x - (mapWidth/2);
        Debug.Log(startingPoint);
        float segmentSize = mapWidth/count;

        Debug.Log(segmentSize);
        float finalPos = startingPoint + segmentSize*index + segmentSize/2;
        Debug.Log(finalPos);
        return finalPos;
    }

    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.Find("GLOBALOBJECT")) {
            game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();
        } else {
            game = GameObject.Find("DEVOBJECT").GetComponent<GlobalController>();
            Debug.Log("Game Loading From Game Scene Directly, USING DEV OBJECT!");
            // Setup Dev Data
            game.map = "Map1";
            game.playType = "Local";
            game.mute = false;
            game.fullScreen = Screen.fullScreen;

        
            foreach (var index in Range(1, 2)) { // Default 2 players
                PlayerData playerData = new PlayerData();
                playerData.name = "player" + index.ToString();
                if (index == 1) {
                    playerData.controllerType = game.wasd;
                    playerData.character = "Andre";
                } else if (index == 2) {
                    playerData.controllerType = game.arrow;
                    playerData.character = "FLLFFL";
                }
                game.players.Add(playerData);
            }
        }
        // Instansiate the Game
        maps = Resources.LoadAll<GameObject>("Maps");
        characters = Resources.LoadAll<GameObject>("Characters");
        healthPrefab = Resources.Load<GameObject>("UI/Health");
        // Load the Map
        map = Instantiate(getMap(game.map)); // load map stored in game.map
        // Load characters and assign values
        int i = 0;
        int defaultHealth = 100;

        foreach (PlayerData playerData in game.players) {
            Debug.Log(playerData.character);
            GameObject newCharacter = Instantiate(getCharacter(playerData.character));
            // Add Name Data
            PlayerController charaData = newCharacter.GetComponent<PlayerController>();
            newCharacter.name = playerData.name;
            charaData.charaName = playerData.character;
            newCharacter.transform.position = new Vector2(getPosFromMap(game.players.Count, i), newCharacter.transform.position.y);

            if (playerData.character == "Andre") {
                playerData.health = Andrehealth; // Assuming Andrehealth is set elsewhere
            } else if (playerData.character == "FLLFFL") {
                playerData.health = FLLFFLhealth; // Assuming FLLFFLhealth is set elsewhere
            } else {
                playerData.health = defaultHealth; // Default health for any other character
            }

            GameObject newCharacterHealthBar = Instantiate(healthPrefab);
            newCharacterHealthBar.transform.position = new Vector2(i*10, newCharacterHealthBar.transform.position.y);

            // Create NameTag
            /** GameObject nameTag = new GameObject();
            nameTag.name = "nameTag";
            TextMeshPro textComponent = nameTag.AddComponent<TextMeshPro>();
            textComponent.text = charaData.playerName;
            textComponent.fontSize = 3;
            nameTag.GetComponent<RectTransform>().sizeDelta = new Vector2(newCharacter.GetComponent<BoxCollider2D>().bounds.size.x, 0.2f);
            nameTag.transform.position = new Vector2(0, 0 + (newCharacter.GetComponent<BoxCollider2D>().bounds.size.y/2.6f));
            nameTag.transform.SetParent(newCharacter.transform, false); */
            
            health = defaultHealth;
            maxHealth = health;
            i++;
        } 

    }
    /*void HealthBarFiller()
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
     
    }*/
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("space key was pressed");
        }
    }

}
