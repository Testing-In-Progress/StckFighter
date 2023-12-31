using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using Unity.VisualScripting;
using UnityEngine;

using static GlobalController;
using TMPro;

public class GameController : MonoBehaviour
{
    public GlobalController game;

    public GameObject[] maps;
    public GameObject map;
    public GameObject[] characters;

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
                    playerData.character = "redstickman";
                } else if (index == 2) {
                    playerData.controllerType = game.arrow;
                    playerData.character = "bluestickman";
                }
                game.players.Add(playerData);
            }
        }
        // Instansiate the Game
        maps = Resources.LoadAll<GameObject>("Maps");
        characters = Resources.LoadAll<GameObject>("Characters");
        // Load the Map
        map = Instantiate(getMap(game.map)); // load map stored in game.map
        // Load characters and assign values
        int i = 0;

        foreach (PlayerData playerData in game.players) {
            GameObject newCharacter = Instantiate(getCharacter(playerData.character));
            // Add Name Data
            PlayerController charaData = newCharacter.GetComponent<PlayerController>();
            charaData.playerName = playerData.name;
            newCharacter.transform.position = new Vector2(getPosFromMap(game.players.Count, i), newCharacter.transform.position.y);

            // Create NameTag
            GameObject nameTag = new GameObject();
            nameTag.name = "nameTag";
            TextMeshPro textComponent = nameTag.AddComponent<TextMeshPro>();
            textComponent.text = charaData.playerName;
            textComponent.fontSize = 3;
            nameTag.GetComponent<RectTransform>().sizeDelta = new Vector2(newCharacter.GetComponent<BoxCollider2D>().bounds.size.x, 0.2f);
            nameTag.transform.position = new Vector2(0, 0 + (newCharacter.GetComponent<BoxCollider2D>().bounds.size.y/2.6f));
            nameTag.transform.SetParent(newCharacter.transform, false);
            
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("space key was pressed");
        }
    }


}
