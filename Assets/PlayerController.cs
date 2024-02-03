using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using static System.Linq.Enumerable;
using System;
using UnityEngine;

using static GlobalController;
using static PlayerData;
using static CharacterBase;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalController game;
    
    public string charaName;
    public CharacterBase selectedCharacter;
    public PlayerData playerData;
    
    public float jump;
    public float xVelocity;
    public float yVelocity;

    public int xDirection;
    public int yDirection;

    void Start()
    {
        if (GameObject.Find("GLOBALOBJECT")) {
            game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();
        } else {
            game = GameObject.Find("DEVOBJECT").GetComponent<GlobalController>();
        }
        game.printData(); // for debugging data pass-through

        FieldInfo characterField = typeof(GlobalController).GetField(charaName);
        Debug.Log(typeof(GlobalController));
        Debug.Log(charaName);
        Debug.Log(characterField);
        selectedCharacter = (CharacterBase)characterField.GetValue(game);

        foreach (PlayerData playerDatae in game.players) {
            if (playerDatae.name == gameObject.name) {
                playerData = playerDatae;
            }
        }

        // jump = 3f;
        xVelocity = 0f;
        yVelocity = 0f;
        
        xDirection = 0;
    }
    void Update(){
        KeyCode leftCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.left);
        KeyCode rightCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.right);
        KeyCode upCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.up);
        KeyCode downCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.down);


        if (Input.GetKeyDown(leftCode)) {
            xDirection = -1;
        }
        if (Input.GetKeyUp(leftCode)) {
            xDirection = 0;
        }


        if (Input.GetKeyDown(rightCode)) {
            xDirection = 1;
        }
        if (Input.GetKeyUp(rightCode)) {
            xDirection = 0;
        }


        if (Input.GetKeyDown(upCode)) {

        }
        if (Input.GetKeyUp(upCode)) {

        }


        if (Input.GetKeyDown(downCode)) {

        }
        if (Input.GetKeyUp(downCode)) {

        }
    }
   /* void processDownInput(string kcode) {
       /* Debug.Log(kcode.ToString() + "\n" + playerData.controllerType.up);
        if (kcode == playerData.controllerType.up) {
            // lookup
        }
        if (kcode == playerData.controllerType.down) {

        }

        if (kcode == playerData.controllerType.left) {
            xDirection = 0;
        }`
        if (kcode == playerData.controllerType.right) {

        }


        if (kcode == playerData.controllerType.jump) {

        }
        if (kcode == playerData.controllerType.attack) {
            selectedCharacter.Attack();
        }
    }
    void processUpInput(string kcode) {
        Debug.Log(kcode.ToString() + "\n" + playerData.controllerType.up);
        if (kcode == playerData.controllerType.up) {
            // lookup
        }
        if (kcode == playerData.controllerType.down) {

        }

        if (kcode == playerData.controllerType.left) {
            xDirection = -1;
        }
        if (kcode == playerData.controllerType.right) {
            xVelocity = +xAccel;
        }

        if (kcode == playerData.controllerType.jump) {
            yVelocity += yAccel;
        }
        if (kcode == playerData.controllerType.attack) {
            selectedCharacter.Attack();
        }
    } */
    // Update is called once per frame
    void FixedUpdate()
    {


        transform.position = new Vector2(transform.position.x + xDirection, transform.position.y + yVelocity);


    }
}
        