using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using System.Linq;
using System;
using UnityEngine;

using static GlobalController;
using static PlayerData;

public class CameraController : MonoBehaviour
{
    public GlobalController game;

    bool playersLoaded;

    List<GameObject> playerObjects;

    public Vector2 bounds;

    public float guider;

    public float minDistance = 5f;
    public float maxDistance = 15f;

    public float zoomSpeed = 5f;

    public Vector2 getCameraBounds() {
        float startBound = playerObjects.First().transform.position.x;
        float endBound = playerObjects.First().transform.position.x;
        //Debug.Log(playerObjects);
        foreach (GameObject playerObject in playerObjects) {
            //Debug.Log(playerObject.name);
            //Debug.Log(playerObject.transform.position.x);
            if (playerObject.transform.position.x < startBound) {
                startBound = playerObject.transform.position.x;
            } else if (playerObject.transform.position.x > endBound) {
                endBound = playerObject.transform.position.x;
            }
        }
        float midPoint = (startBound + endBound)/2;
        //Debug.Log(midPoint);
        if (Math.Abs(endBound-startBound) < minDistance) {
            startBound = midPoint - minDistance/2;
            endBound = midPoint + minDistance/2;
        }
        if (Math.Abs(endBound-startBound) > maxDistance) {
            startBound = midPoint - maxDistance/2;
            endBound = midPoint + maxDistance/2;
        }
        return new Vector2(startBound, endBound);
    }

    // Start is called before the first frame update
    void Start()
    {
        guider = 0f;
        if (GameObject.Find("GLOBALOBJECT")) {
            game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();
        } else {
            game = GameObject.Find("DEVOBJECT").GetComponent<GlobalController>();
            Debug.Log("Game Loading From Game Scene Directly, USING DEV OBJECT!");
            Debug.Log(game.players);
            Debug.Log(game.map);
            // Setup Dev Data
            game.map = "Map1";
            game.playType = "Local";
            game.mute = false;
            game.fullScreen = Screen.fullScreen;
            game.players = new List<PlayerData>();
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

        playersLoaded = false;
        playerObjects = new List<GameObject>();
        
        Debug.Log(game);
        Debug.Log(game.players);
        Debug.Log(game.map);

        bounds = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (game == null || game.players == null)
        {
            Debug.LogError("Game or game.players is null");
            return;
        }
        if (playersLoaded == false) {
            foreach (PlayerData playerData in game.players) {
                playersLoaded = true;
                Debug.Log(playerData.name);
                if (GameObject.Find(playerData.name)) {
                    Debug.Log(GameObject.Find(playerData.name));
                    playerObjects.Add(GameObject.Find(playerData.name));
                    
                } else {
                    playersLoaded = false;
                }
                
                
                Debug.Log(";dsafhsudflk vjadsnfhvlaksd fvkjbrdilufgabslknhkavsj ethan vu was here");
            }
            if (playersLoaded == true) {
                Debug.Log(getCameraBounds().x);
                Debug.Log(getCameraBounds().y);
                bounds = getCameraBounds();
            }
        } else {
            bounds = getCameraBounds();    // left        right

            float distance = Mathf.Clamp(Math.Abs(bounds.y-bounds.x), minDistance, maxDistance);
            Debug.Log(distance);

            float newOrthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, distance-guider, Time.deltaTime * zoomSpeed);
            Debug.Log(newOrthographicSize);
            GetComponent<Camera>().orthographicSize = newOrthographicSize;
            
            gameObject.transform.position = new Vector3( (bounds.x + bounds.y)/2, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        /***/
    }
}