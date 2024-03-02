using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject canvas;
    public Gradient gradient;



  /*  public TMPro.TextMeshProUGUI healthText;
    public Image healthBar;*/
    float health, maxHealth = 100;
    float lerpSpeed = 1f;

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

        
            if (game.players == null || game.players.Count == 0) {
                game.players = new List<PlayerData>();
                foreach (var index in Range(1, 2)) { // Default 2 players
                    PlayerData playerData = new PlayerData();
                    Debug.Log(playerData);
                    playerData.name = "player" + index.ToString();
                    if (index == 1) {
                        playerData.controllerType = game.wasd;
                        playerData.character = "Andre";
                        playerData.health = 0;
                    } else if (index == 2) {
                        playerData.controllerType = game.arrow;
                        playerData.character = "FLLFFL";
                        playerData.health = 0;
                    }
                    Debug.Log(playerData);
                    game.players.Add(playerData);
                }
            }
        }
        // Instansiate the Game
        maps = Resources.LoadAll<GameObject>("Maps");
        characters = Resources.LoadAll<GameObject>("Characters");
        canvas = GameObject.Find("Canvas");
        healthPrefab = Resources.Load<GameObject>("UI/healthBar");
        // Load the Map
        map = Instantiate(getMap(game.map)); // load map stored in game.map
        // Load characters and assign values
        int i = 0;
        int defaultHealth = 100;

        Debug.Log(game.players.Count);

        foreach (PlayerData playerData in game.players) {
            Debug.Log(playerData.character);
            GameObject newCharacter = Instantiate(getCharacter(playerData.character));
            // Add Name Data
            PlayerController charaData = newCharacter.GetComponent<PlayerController>();
            newCharacter.name = playerData.name;
            charaData.charaName = playerData.character;
            newCharacter.transform.position = new Vector2(getPosFromMap(game.players.Count, i), newCharacter.transform.position.y);
            newCharacter.GetComponent<Animator>().SetBool("menu", false);

            GameObject newCharacterHealthBar = Instantiate(healthPrefab);
            newCharacterHealthBar.transform.parent = canvas.transform;
            newCharacterHealthBar.GetComponent<RectTransform>().localScale = new Vector2(1,1);
            newCharacterHealthBar.transform.localPosition = new Vector3((i*(canvas.GetComponent<RectTransform>().sizeDelta.x/2))-(canvas.GetComponent<RectTransform>().sizeDelta.x/4), (canvas.GetComponent<RectTransform>().sizeDelta.y/2)-(newCharacterHealthBar.GetComponent<RectTransform>().sizeDelta.y/1.25f), 0);
            newCharacterHealthBar.name = playerData.name + "HealthBar";
            Debug.Log(newCharacterHealthBar.transform.position.z);
            Debug.Log(newCharacterHealthBar.transform.localPosition.z);

            // Create NameTag
            /** GameObject nameTag = new GameObject();
            nameTag.name = "nameTag";
            TextMeshPro textComponent = nameTag.AddComponent<TextMeshPro>();
            textComponent.text = charaData.playerName;
            textComponent.fontSize = 3;
            nameTag.GetComponent<RectTransform>().sizeDelta = new Vector2(newCharacter.GetComponent<BoxCollider2D>().bounds.size.x, 0.2f);
            nameTag.transform.position = new Vector2(0, 0 + (newCharacter.GetComponent<BoxCollider2D>().bounds.size.y/2.6f));
            nameTag.transform.SetParent(newCharacter.transform, false); */
            
            i++;
        } 
        SetHealthBarFiller();
    }

    void SetHealthBarFiller()
    {
        foreach (PlayerData player in game.players) {
            Slider slider = GameObject.Find(player.name + "HealthBar").GetComponent<Slider>();
            slider.value = Mathf.Lerp(slider.value, (player.health), lerpSpeed);
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
        SetHealthBarFiller();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log(game.players[0].health);
            game.players[0].health = game.players[0].health - 10;
        }

        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            Debug.Log(game.players[0].health);
            game.players[0].health = game.players[0].health + 10;
        } 
    }

}
