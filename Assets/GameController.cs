using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static GlobalController;
using static CharacterBase;
using TMPro;

public class GameController : MonoBehaviour
{
    public GlobalController game;
    public GameObject[] maps;
    public GameObject map;
    public GameObject healthPrefab;
    public GameObject specialPrefab;
    public GameObject[] characters;
    public GameObject canvas;
    public Gradient gradient;



    float health, maxHealth = 100;
    float special, maxSpecial = 100;
    float lerpSpeed = 0.1f;

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
                        playerData.health = 100;
                        playerData.special = 0;
                    } else if (index == 2) {
                        playerData.controllerType = game.arrow;
                        playerData.character = "FLLFFL";
                        playerData.health = 100;
                        playerData.special = 0;
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
        specialPrefab = Resources.Load<GameObject>("UI/specialBar");
        // Load the Map
        map = Instantiate(getMap(game.map)); // load map stored in game.map
        map.name = "gameMap";
        // Load characters and assign values
        int i = 0;
        int defaultHealth = 100;
        int defaultSpecial = 0;

        Debug.Log(game.players.Count);

        List<GameObject> charaObjects = new List<GameObject>();

        foreach (PlayerData playerData in game.players) {
            Debug.Log(playerData.character);
            GameObject newCharacter = Instantiate(getCharacter(playerData.character));
            // Add Name Data
            PlayerController charaData = newCharacter.GetComponent<PlayerController>();
            newCharacter.name = playerData.name;
            charaData.charaName = playerData.character;
            newCharacter.transform.position = new Vector2(getPosFromMap(game.players.Count, i), newCharacter.transform.position.y);
            newCharacter.GetComponent<Animator>().SetBool("menu", false);

            charaObjects.Add(newCharacter);

            // Instansiate healthBar
            GameObject newCharacterHealthBar = Instantiate(healthPrefab);
            newCharacterHealthBar.transform.parent = canvas.transform;
            newCharacterHealthBar.GetComponent<RectTransform>().localScale = new Vector2(1,1);
            newCharacterHealthBar.transform.localPosition = new Vector3((i*(canvas.GetComponent<RectTransform>().sizeDelta.x/2))-(canvas.GetComponent<RectTransform>().sizeDelta.x/4), (canvas.GetComponent<RectTransform>().sizeDelta.y/2)-(newCharacterHealthBar.GetComponent<RectTransform>().sizeDelta.y/1.25f), 0);
            newCharacterHealthBar.name = playerData.name + "HealthBar";
            Debug.Log(newCharacterHealthBar.transform.position.z);
            Debug.Log(newCharacterHealthBar.transform.localPosition.z);
            if ((i+1)/game.players.Count > 0.5f) {
                Debug.Log(newCharacterHealthBar.transform.Find("Border").gameObject.GetComponent<RectTransform>().sizeDelta.x);
                newCharacterHealthBar.transform.Find("Heart").localPosition = new Vector3(newCharacterHealthBar.transform.Find("Heart").localPosition.x+newCharacterHealthBar.transform.Find("Border").gameObject.GetComponent<RectTransform>().sizeDelta.x, newCharacterHealthBar.transform.Find("Heart").localPosition.y, newCharacterHealthBar.transform.Find("Heart").localPosition.z);
            }

            // Instansiate specialBar
            GameObject newCharacterSpecialBar = Instantiate(specialPrefab);
            newCharacterSpecialBar.transform.parent = canvas.transform;
            newCharacterSpecialBar.GetComponent<RectTransform>().localScale = new Vector2(1,1);
            newCharacterSpecialBar.transform.localPosition = new Vector3((i*(canvas.GetComponent<RectTransform>().sizeDelta.x-((newCharacterSpecialBar.GetComponent<RectTransform>().sizeDelta.x/2)*3)))-(canvas.GetComponent<RectTransform>().sizeDelta.x/2)+((newCharacterSpecialBar.GetComponent<RectTransform>().sizeDelta.x/4)*3), (canvas.GetComponent<RectTransform>().sizeDelta.y/2*-1)+(newCharacterHealthBar.GetComponent<RectTransform>().sizeDelta.y/1.25f), 0);
            newCharacterSpecialBar.name = playerData.name + "SpecialBar";
            Debug.Log(newCharacterSpecialBar.transform.position.z);
            Debug.Log(newCharacterSpecialBar.transform.localPosition.z);
            Debug.Log(i);
            Debug.Log(game.players.Count);
            Debug.Log((i+1)/game.players.Count);
            if ((i+1)/game.players.Count > 0.5f) {
                Debug.Log(newCharacterSpecialBar.transform.Find("Border").gameObject.GetComponent<RectTransform>().sizeDelta.x);
                newCharacterSpecialBar.transform.Find("Lightning").localPosition += new Vector3(newCharacterSpecialBar.transform.Find("Border").gameObject.GetComponent<RectTransform>().sizeDelta.x, 0, 0);
            }

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
        game.players[0].health = (int)maxHealth;
        game.players[1].health = (int)maxHealth; // 
        SetHealthBarFiller();

        game.players[0].special = (int)defaultSpecial;
        game.players[1].special = (int)defaultSpecial;
        SetSpecialBarFiller();

        // Ignore collisions between players
        for (int x = 0; x < charaObjects.Count; x++)
        {
            // Loop through subsequent numbers to form combinations
            for (int j = x + 1; j < charaObjects.Count; j++)
            {
                // Print out the combination
                Debug.Log(charaObjects[x].GetComponent<BoxCollider2D>());
                Debug.Log(charaObjects[j].GetComponent<BoxCollider2D>());
                Physics2D.IgnoreCollision(charaObjects[x].GetComponent<BoxCollider2D>(), charaObjects[j].GetComponent<BoxCollider2D>());
            }
        }
    }

    void SetHealthBarFiller()
    {
    
        foreach (PlayerData player in game.players) {
            if(0 < player.health){
                Slider slider = GameObject.Find(player.name + "HealthBar").GetComponent<Slider>();
                slider.value = Mathf.Lerp(slider.value, (player.health), lerpSpeed);
            }
            else{
                SceneManager.LoadScene("MainMenu");
            }
        }
        
    }

    void SetSpecialBarFiller()
    {
    
        foreach (PlayerData player in game.players) {
            if(0 < player.special || 100 >= player.special){
                Slider slider = GameObject.Find(player.name + "SpecialBar").GetComponent<Slider>();
                slider.value = Mathf.Lerp(slider.value, (player.special), lerpSpeed);
            }
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
        SetSpecialBarFiller();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log(game.players[0].special);
            game.players[1].special = game.players[0].special - 10;
            
        }

        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            Debug.Log(game.players[0].special);
            game.players[1].special = game.players[1].special + 10;
        } // now ittl change falafel's special. so load the game, then make ffafles specail at 0, then test specail
    }

}
