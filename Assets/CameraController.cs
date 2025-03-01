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
    GameObject leftBorder;
    GameObject rightBorder;

    public Vector2 bounds;

    public float guider;

    public float minDistance = 5f;
    public float maxDistance = 11f;

    public float zoomSpeed = 5f;

    public float adjustYCamera = 5;
    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;



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
        bottomLimit = 7f;
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
            if (game.players == null || game.players.Count == 0) {
                game.players = new List<PlayerData>();
                foreach (var index in Range(1, 2)) { // Default 2 players
                    PlayerData playerData = new PlayerData();
                    Debug.Log(playerData);
                    playerData.name = "player" + index.ToString();
                    if (index == 1) {
                        playerData.controllerType = game.wasd;
                        playerData.character = "Andre";
                    } else if (index == 2) {
                        playerData.controllerType = game.arrow;
                        playerData.character = "FLLFFL";
                    }
                    Debug.Log(playerData);
                    game.players.Add(playerData);
                }
            }
        }

        playersLoaded = false;
        playerObjects = new List<GameObject>();
        
        Debug.Log(game);
        Debug.Log(game.players);
        Debug.Log(game.map);

        bounds = new Vector2(0,0);
        // set left and right border to the children of this object with the names lefborder and rightborder
        leftBorder = gameObject.transform.Find("leftBorder").gameObject;
        rightBorder = gameObject.transform.Find("rightBorder").gameObject;
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
            bottomLimit = GameObject.Find("gameMap").transform.Find("mainFloor").transform.position.y;
            leftLimit = GameObject.Find("LeftWall").transform.position.x;
            Debug.Log(" fijdsfjdsfjlfafhioraegh peruaefhare left" + leftLimit);
            //leftLimit = -3.53f;
            rightLimit = GameObject.Find("RightWall").transform.position.x;
            Debug.Log("fijdsfjdsfjlfaffoeffjioihioraegh right" + rightLimit);
        } else {
            bounds = getCameraBounds();    // left        right

            float distance = Mathf.Clamp(Math.Abs(bounds.y-bounds.x), minDistance, maxDistance);
            //Debug.Log(distance);

            bool aPlayerJumping = false;
            foreach (GameObject playerObject in playerObjects) {
                if (playerObject.GetComponent<PlayerController>().onGround == false) {
                    aPlayerJumping = true;
                }
            }
            if (aPlayerJumping) {
                float newOrthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, distance-guider, Time.deltaTime * zoomSpeed);
                //Debug.Log(distance);
                GetComponent<Camera>().orthographicSize = newOrthographicSize;
            } else {
                float newOrthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, minDistance-guider, Time.deltaTime * zoomSpeed);
                //Debug.Log(minDistance);
                GetComponent<Camera>().orthographicSize = newOrthographicSize;
            }
            
            gameObject.transform.position = new Vector3(
                Mathf.Clamp((bounds.x + bounds.y)/2,leftLimit,rightLimit), bottomLimit + GetComponent<Camera>().orthographicSize, gameObject.transform.position.z);
            // set the leftborder and rightborder to the current bounds of the camera
            leftBorder.transform.position = new Vector3(bounds.x, leftBorder.transform.position.y, leftBorder.transform.position.z);
            rightBorder.transform.position = new Vector3(bounds.y, gameObject.transform.position.y, rightBorder.transform.position.z);
            /*gameObject.transform.position = Vector3(Mathf.Clamp(gameObject.transform.position.x,leftLimit, rightLimit), 
            (Mathf.Clamp(gameObject.transform.position.y,topLimit, bottomLimit), transform.position.z));*/
        }

        /***/
    }
    
}