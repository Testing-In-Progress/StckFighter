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

    public bool four;
    public bool six;
    public bool eight;
    public bool two;

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
        xVelocity = 0.1f;
        yVelocity = 0f;
        
        xDirection = 0;
        yDirection = 0;

        four = false;
        six = false;
        eight = false;
        two = false;
    }
    void Update(){
        KeyCode leftCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.left);
        KeyCode rightCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.right);
        KeyCode upCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.up);
        KeyCode downCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.down);
        KeyCode jumpCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.jump);

        // To detect what direction or input the player is doing
        if (Input.GetKey(leftCode)) {
            four = true;
        }
        else{
            four = false;
        }
        
        if (Input.GetKey(rightCode)) {
            six = true;
        }
        else{
            six = false;
        }

        if (Input.GetKey(upCode)) {
            eight = true;
        }
        else{
            eight = false;
        }

        if (Input.GetKey(downCode)) {
            two = true;
        }
        else{
            two = false;
        }
        if (Input.GetKey(jumpCode)) {
            four = true;
        }
        else{
            four = false;
        }
        

        // Defines inputs into movement
        if (four == true && six == false){
            xDirection = -1;
        }
        else if (four == false && six == true){
            xDirection = 1;
        }
        else if (four == false && six == false){
            xDirection = 0;
        }
        else if (four == true && six == true){
            xDirection = 0;
        }
        
    }
    void FixedUpdate()
    {


        transform.position = new Vector2(transform.position.x + xDirection * xVelocity, transform.position.y + yVelocity);


    }
}
        