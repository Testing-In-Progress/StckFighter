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
    public float crouchSpeed;
    public float sprintMultiplier;
    public float xVelocity;
    public float yAccel;
    public float maxHeight;
    public float initialSpeedY;
    public float yVelocity;

    
    public int xDirection;
    public int dashDirection;
    public int airDirection;


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
    public bool isAirDashing;
    public bool hasAirDashed;
    public bool enemyPositionOnLeft;
    public bool sprint;
    public bool shield;

    public float backDashDistance;
    public float forwardDashDistance;
    public float backDashInitialSpeed;
    public float forwardDashInitialSpeed;
    public float backDashTime;
    public float forwardDashTime;

    public bool groundForwardDash;
    public bool groundBackDash;
    public bool airForwardDash;
    public bool airBackDash;

    public float jumpBufferTime;
    public float airDashDelayTime;
    public float dashRefreshTime;
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
            Debug.Log(playerDatae.name);
            Debug.Log(gameObject.name);
            if (playerDatae.name == gameObject.name) {
                playerData = playerDatae;
            }
        }

        // jump = 3f;
        walkSpeed = 6f;
        crouchSpeed = 0f;
        sprintMultiplier = 3f;

        xVelocity = 0f;
        yAccel = 6f;
        maxHeight = 60f;     

        xDirection = 0;
        dashDirection = 0;
        airDirection = 0;
    

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
        isAirDashing = false;
        hasAirDashed = false;

        backDashInitialSpeed = 50f;
        forwardDashInitialSpeed = 100f;
        backDashDistance = 3f;
        forwardDashDistance = 6f;
        airDashDelayTime = 0.25f;

        groundForwardDash = false;
        groundBackDash = false;
        airForwardDash = false;
        airBackDash = false;

        dashRefreshTime = 0.25f;
        jumpBufferTime = 0.1f;
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
        backDashTime = backDashDistance / backDashInitialSpeed;
        forwardDashTime = forwardDashDistance / forwardDashInitialSpeed;
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
        else if (up == false && down == true && onGround){
            crouch = true;
            lookUp = false;
        }
        else{
            crouch = false;
            lookUp = false;
        }
        if (crouch == true && onGround == true){
            xVelocity = crouchSpeed;
        }
        else if (sprint == true && onGround == true && enemyPositionOnLeft == true && left == true){
            xVelocity = walkSpeed * sprintMultiplier;
        }
        else if (sprint == true && onGround == true && enemyPositionOnLeft == false && right == true){
            xVelocity = walkSpeed * sprintMultiplier;
        }
        else{
            xVelocity = walkSpeed;
        }

        if (jump == true && onGround == true){
            jumpFunction();
        }

        if (dash == true && canDash == true && isDashing == false && xDirection != 0){
            dashFunction();
        }

        if (left == true && enemyPositionOnLeft == false){
            shield = true;
        }
        else if (right == true && enemyPositionOnLeft == true){
            shield = true;
        }
        else if (Input.GetKey(dashCode) && xDirection == 0){
            sprint = false;
            shield = true;
        }
        else{
            shield = false;
        }
        if (hasAirDashed == true && onGround == true){
                Invoke("refreshDashCooldown", dashRefreshTime);
                refreshMoveCooldown();
                hasAirDashed = false;
                isAirDashing = false;
            }
        if (onGround == true && groundBackDash == false){
            refreshMoveCooldown();
        }
        
    }
    void FixedUpdate()
    {
   
        if (isDashing == false && canMove == true){
            characterRB.gravityScale = yAccel;
            characterRB.velocity = new Vector2(xDirection * xVelocity, characterRB.velocity.y);
        }
        else if (isDashing == false && canMove == false){
            characterRB.gravityScale = yAccel;
        }
        else{
            characterRB.velocity = new Vector2(0, 0);
            characterRB.gravityScale = 0f;
        }

        if (isDashing == true && groundForwardDash == true){
            characterRB.velocity = new Vector2(dashDirection * forwardDashInitialSpeed, 0);
            Invoke("stopDash", forwardDashTime);

        }
        else if (isDashing == true && groundBackDash == true){
            characterRB.velocity = new Vector2(dashDirection * backDashInitialSpeed, 0);
            Invoke("stopDash", backDashTime);
        }
        else if (isDashing == true && airForwardDash == true){
            Invoke("airDashStart", airDashDelayTime);
        }
        else if (isDashing == true && airBackDash == true){
            Invoke("airDashStart", airDashDelayTime);
        }

        if (isAirDashing == true && airForwardDash == true){
            characterRB.velocity = new Vector2(dashDirection * forwardDashInitialSpeed, 0);
            Invoke("stopDash", forwardDashTime);
        }
        else if (isAirDashing == true && airBackDash == true){
            characterRB.velocity = new Vector2(dashDirection * backDashInitialSpeed, 0);
            Invoke("stopDash", backDashTime);
        }



        
        

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
            canMove = false;
        }
    }

    public void jumpBuffer(){
        onGround = true; 
    }
    public void jumpFunction(){
        characterRB.velocity = new Vector2(characterRB.velocity.x, initialSpeedY);
    }
    public void dashFunction(){
        canDash = false;
        isDashing = true;
        if (xDirection == -1 && onGround == true && enemyPositionOnLeft == true){
            //forward ground
            dashDirection = -1;
            groundForwardDash = true;

        }
        else if (xDirection == 1 && onGround == true && enemyPositionOnLeft == false){
            //forward ground
            dashDirection = 1;
            groundForwardDash = true;

        }
        else if (xDirection == 1 && onGround == true && enemyPositionOnLeft == true){
            //backward ground
            dashDirection = 1;
            groundBackDash = true;
        }
        else if (xDirection == -1 && onGround == true && enemyPositionOnLeft == false){
            //backward ground
            dashDirection = -1;
            groundBackDash = true;

        }
        else if (xDirection == -1 && onGround == false && enemyPositionOnLeft == true){
            //forward air
            dashDirection = -1;
            airForwardDash = true;

        }
        else if (xDirection == 1 && onGround == false && enemyPositionOnLeft == false){
            //forward air
            dashDirection = 1;
            airForwardDash = true;

        }
        else if (xDirection == 1 && onGround == false && enemyPositionOnLeft == true){
            //backward air
            dashDirection = 1;
            airBackDash = true;

        }
        else if (xDirection == -1 && onGround == false && enemyPositionOnLeft == false){
            //backward air
            dashDirection = -1;
            airBackDash = true;

        }
        
        

    }
    public void stopDash(){
        if (groundForwardDash == true){
            groundForwardDash = false;
            isDashing = false;
            Invoke("refreshDashCooldown", dashRefreshTime);
            refreshMoveCooldown();
        }
        else if (groundBackDash == true){
            groundBackDash = false;
            isDashing = false;
            Invoke("refreshMoveCooldown", dashRefreshTime);
            Invoke("refreshDashCooldown", dashRefreshTime);
        }
        else if (airBackDash == true){
            airBackDash = false;
            isDashing = false;
            isAirDashing = false;
            characterRB.velocity = new Vector2(dashDirection * xVelocity, characterRB.velocity.y);
            hasAirDashed = true;

        }
        else if (airForwardDash == true){
            airForwardDash = false;
            isDashing = false;
            isAirDashing = false;
            characterRB.velocity = new Vector2(dashDirection * xVelocity, characterRB.velocity.y);
            hasAirDashed = true;
        }
    }
    public void airDashStart(){
        isAirDashing = true;
    }
    public void refreshDashCooldown(){
        canDash = true;
    }
    public void refreshMoveCooldown(){
        canMove = true;
    }

}
        