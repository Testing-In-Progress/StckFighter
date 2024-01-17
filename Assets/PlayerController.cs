using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;
using System;
using UnityEngine;

using static GlobalController;
using static PlayerData;

using static redstickman;
using static bluestickman;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalController game;
    
    public string charaName;
    public PlayerData playerData;
    
    public float jump;
    public float xVelocity;
    public float yVelocity;
    public float xAccel;
    public float yAccel;
    public float xDrag;
    public float yDrag;

    public redstickman redstickman;
    public bluestickman bluestickman;

    void Start()
    {
        if (GameObject.Find("GLOBALOBJECT")) {
            game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();
        } else {
            game = GameObject.Find("DEVOBJECT").GetComponent<GlobalController>();
        }
        game.printData(); // for debugging data pass-through


        foreach (PlayerData playerDatae in game.players) {
            if (playerDatae.name == gameObject.name) {
                playerData = playerDatae;
            }
        }

        // jump = 3f;
        //xVelocity = 0;
        //yVelocity = 0;
        xAccel = 0.03f;
        //yAccel = 0.01f;
        xDrag = 4;
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
        if (kcode == playerData.controllerType.jump) {
            this.GetType().GetField(charaName).GetValue(this).Jump();
        }
        if (kcode == playerData.controllerType.attack) {
            this.GetType().GetField(charaName).GetValue(this).Jump();
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
        