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
    public Rigidbody2D characterRB;
    public BoxCollider2D feet;
    public float jump;
    public float walkSpeed;
    public float jumpHeight;
    public float xVelocity;
    public float yAccel;
    public float yVelocity;

    
    public int xDirection;
    public int yDirection;

    public bool left;
    public bool right;
    public bool up;
    public bool down;
    public bool jumpX;
    public bool crouch; 
    public bool lookUp;
    public bool onGround;
    
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
        walkSpeed = 0.2f;
        jumpHeight = 20f;
        xVelocity = 0f;
        yAccel = 3f;
        
        xDirection = 0;
        yDirection = 0;

        left = false;
        right = false;
        up = false;
        down = false;
        jumpX = false;
        crouch = false; 
        onGround = false;
    }
    void Update(){
        yVelocity = characterRB.velocity.y;
     

        KeyCode leftCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.left);
        KeyCode rightCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.right);
        KeyCode upCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.up);
        KeyCode downCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.down);
        KeyCode jumpCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.jump);

        // To detect what direction or input the player is doing
        if (Input.GetKey(leftCode)) {
            left = true;
        } else {
            left = false;
        }
        
        if (Input.GetKey(rightCode)) {
            right = true;
        }
        else{
            right = false;
        }

        if (Input.GetKey(upCode)) {
            up = true;
        }
        else{
            up = false;
        }

        if (Input.GetKey(downCode)) {
            down = true;
        }
        else{
            down = false;
        }
        if (Input.GetKey(jumpCode)) {
            jumpX = true;
        }
        else{
            jumpX = false;
        }
        

        // Defines inputs into movement
        if (left == true && right == false){
            xDirection = -1;
        }
        else if (left == false && right == true){
            xDirection = 1;
        }
        else if (left == false && right == false){
            xDirection = 0;
        }
        else if (left == true && right == true){
            xDirection = 0;
        }

        if (up == true && down == false){
            crouch = false;
            lookUp = true;
        }
        else if (up == false && down == true){
            crouch = true;
            lookUp = false;
        }
        else if (up == false && down == false){
            crouch = false;
            lookUp = false;
        }
        else if (up == true && down == true){
            crouch = false;
            lookUp = false;
        }

        if (crouch == true && onGround == true){
            xVelocity = walkSpeed / 4;
        }
        else{
            xVelocity = walkSpeed;
        }

        if (jumpX == true && onGround == true){
            yDirection = 1;
            jumpFunction();
        }
        else{
            yDirection = 0;
        }
        
    }
    void FixedUpdate()
    {

        characterRB.gravityScale = yAccel;
        transform.position = new Vector2(transform.position.x + xDirection * xVelocity, transform.position.y);


    }
    public void OnTriggerEnter2D(Collider2D feet)
    {
        // Check if the collider is tagged as ground
        if (feet.CompareTag("ground"))
        {
            onGround = true; 
        }
        
    }

    public void OnTriggerExit2D(Collider2D feet)
    {
        // Check if the collider is tagged as ground
        if (feet.CompareTag("ground"))
        {
            onGround = false; 
        }
    }
    public void jumpFunction(){
        characterRB.velocity = new Vector2(characterRB.velocity.x, jumpHeight);
    }
}
        