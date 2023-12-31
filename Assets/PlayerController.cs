using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using System;
using UnityEngine;

using static GlobalController;
using static PlayerData;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalController game;
    
    public string playerName;
    public PlayerData playerData;
    
    public float jump;
    public float xVelocity;
    public float yVelocity;
    public float xAccel;
    public float yAccel;
    public float xDrag;
    public float yDrag;
    void Start()
    {
        if (GameObject.Find("GLOBALOBJECT")) {
            game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();
        } else {
            game = GameObject.Find("DEVOBJECT").GetComponent<GlobalController>();
        }
        game.printData(); // for debugging data pass-through

        foreach (PlayerData playerDatae in game.players) {
            if (playerDatae.name == playerName) {
                playerData = playerDatae;
            }
        }

        // jump = 3f;
        //xVelocity = 0;
        //yVelocity = 0;
        //xAccel = 0.01f;
        //yAccel = 0.01f;
        //xDrag = 2;
        //yDrag = 2;
    }
    void processInput(string kcode) {
        Debug.Log(kcode.ToString() + "\n" + playerData.controllerType.up);
        if (kcode == playerData.controllerType.up) {
            yVelocity += yAccel;
        }
        if (kcode == playerData.controllerType.down) {
            yVelocity -= yAccel;
        }
        if (kcode == playerData.controllerType.left) {
            xVelocity -= xAccel;
        }
        if (kcode == playerData.controllerType.right) {
            xVelocity += xAccel;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!game.paused && Input.anyKey) {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    processInput(kcode.ToString());
                }
            }
        }

        transform.position = new Vector2(transform.position.x + xVelocity, transform.position.y + yVelocity);

        xVelocity = xVelocity/xDrag;
        yVelocity = yVelocity/yDrag;
    }
}
        