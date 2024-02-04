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
    public Transform opponent;
    public PlayerData playerData;
    public Rigidbody2D characterRB;
    public BoxCollider2D feet;

    public float walkSpeed;
    public float xVelocity;
    public float yAccel;
    public float maxHeight;
    public float initialSpeedY;
    public float yVelocity;

    
    public int xDirection;
    public int dashDirection;
    public int yDirection;

    public bool left;
    public bool right;
    public bool up;
    public bool down;
    public bool jump;
    public bool crouch; 
    public bool lookUp;
    public bool onGround;
    public bool canMove;

    public bool dash;
    public bool canDash;
    public bool isDashing;
    public bool enemyPositionOnLeft;
    public bool sprint;
    public bool shield;
    
    public float jumpBufferTime;
    void Start()
    {
        if (GameObject.Find("GLOBALOBJECT")) {
            game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();
        } else {
            game = GameObject.Find("DEVOBJECT").GetComponent<GlobalController>();
        }
        game.printData(); // for debugging data pass-through

        if (game.players.Count == 2) {
            foreach(PlayerData player in game.players) {
                if (player.name != gameObject.name) {
                    opponent = GameObject.Find(player.name).transform;
                }
            }
        }

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

        xVelocity = 0f;
        yAccel = 6f;
        maxHeight = 60f;     

        xDirection = 0;
        dashDirection = 0;
        yDirection = 0;

        left = false;
        right = false;
        up = false;
        down = false;
        jump = false;
        crouch = false; 
        onGround = false;
        canMove = true;

        dash = false;
        sprint = false;
        shield = false;

        canDash = true;
        isDashing = false;

        jumpBufferTime = 0.5f;
    }
    void Update(){
        if (opponent.position.x < transform.position.x){
            enemyPositionOnLeft = true;
        }
        else{
            enemyPositionOnLeft = false;
        }
        
        yVelocity = characterRB.velocity.y;
        initialSpeedY = Mathf.Sqrt(2f * yAccel * maxHeight);

        KeyCode leftCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.left);
        KeyCode rightCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.right);
        KeyCode upCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.up);
        KeyCode downCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.down);
        KeyCode jumpCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.jump);
        KeyCode dashCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), playerData.controllerType.dash);

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
            jump = true;
        }
        else{
            jump = false;
        }
        if (Input.GetKeyDown(dashCode)) {
            dash = true;
        }
        else{
            dash = false;
        }
        if (Input.GetKey(dashCode) && xDirection != 0){
            sprint = true;
            shield = false;
        }
        else if (Input.GetKey(dashCode) && xDirection == 0){
            sprint = false;
            shield = true;
        }
        else{
            sprint = false;
            shield = false;
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

        if (jump == true && onGround == true){
            yDirection = 1;
            jumpFunction();
        }
        else{
            yDirection = 0;
        }

        if (dash == true && canDash == true && isDashing == false  && xDirection != 0){

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
            Invoke("jumpBuffer", jumpBufferTime);
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

    public void jumpBuffer(){
        onGround = true; 
    }
    public void jumpFunction(){
        characterRB.velocity = new Vector2(characterRB.velocity.x, initialSpeedY);
    }
    public void groundForwardDash(){

    }
    public void groundBackDash(){

    }
    public void airForwardDash(){

    }
    public void airBackDash(){

    }
    public void refreshDashCooldown(){

    }

}
        