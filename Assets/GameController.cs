using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using Unity.VisualScripting;
using UnityEngine;

using static GlobalController;

public class GameController : MonoBehaviour
{
    public GlobalController game;

    public GameObject[] maps;
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
        GameObject map = Instantiate(getMap(game.map)); // load map stored in game.map
        // Load characters and assign values
        int index = 0;

        foreach (PlayerData playerData in game.players) {
            GameObject newCharacter = Instantiate(getCharacter(playerData.character));
            PlayerController charaData = newCharacter.GetComponent<PlayerController>();
            charaData.playerName = playerData.name;
            newCharacter.transform.position = new Vector2(getPosFromMap(game.players.Count, index), newCharacter.transform.position.y);
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
