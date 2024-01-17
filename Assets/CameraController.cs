using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using System.Linq;
using UnityEngine;

using static GlobalController;
using static PlayerData;

public class CameraController : MonoBehaviour
{
    public GlobalController game;

    public GameObject[] playerObjects;

    public Vector2 bounds;

    public float minDistance = 5f;
    public float maxDistance = 15f;
    
    public float zoomSpeed = 5f;


    public Vector2 getCameraBounds() {
        float startBound = -minDistance;
        float endBound = minDistance;
        foreach (GameObject playerObject in playerObjects) {
            if (playerObject.transform.position.x < startBound) {
                startBound = playerObject.transform.position.x;
            } else if (playerObject.transform.position.x > startBound) {
                endBound = playerObject.transform.position.x;
            }
        }
        if (startBound < -maxDistance) {
            startBound = -maxDistance;
        }
        if (endBound < maxDistance) {
            endBound = maxDistance;
        }
        return new Vector2(startBound, endBound);
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
                Debug.Log(playerData);
                playerData.name = "player" + index.ToString();
                if (index == 1) {
                    playerData.controllerType = game.wasd;
                    playerData.character = "redstickman";
                } else if (index == 2) {
                    playerData.controllerType = game.arrow;
                    playerData.character = "bluestickman";
                }
                Debug.Log(playerData);
                game.players.Add(playerData);
            }
        }
        
        Debug.Log(game);
        Debug.Log(game.players);
        Debug.Log(game.map);
        foreach (PlayerData playerData in game.players) {
            Debug.Log(GameObject.Find(playerData.name));
            playerObjects.Append(GameObject.Find(playerData.name));
            Debug.Log(";dsafhsudflk vjadsnfhvlaksd fvkjbrdilufgabslknhkavsj ethan vu was here");
        }
    }

    // Update is called once per frame
    void Update()
    {
        bounds = getCameraBounds();    // left        right
        gameObject.transform.position = new Vector3( (bounds.x + bounds.y)/2, gameObject.transform.position.y, gameObject.transform.position.z);

        /**float distance = Mathf.Clamp((bounds.x + bounds.y)/2, minDistance, maxDistance);

        float targetX = (bounds.x + bounds.y) / 2;
        float newX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * 5f); // Adjust the speed as needed

        float newOrthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, distance, Time.deltaTime * zoomSpeed);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        GetComponent<Camera>().orthographicSize = newOrthographicSize;*/
    }
}