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
    public Animator anim;
    public SpriteRenderer spriteRenderer;

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
    public bool knocked;

    public bool dash;
    public bool canDash;
    public bool isDashing;
    public bool isAirDashing;
    public bool hasAirDashed;
    public bool attacking;
    public bool isLockedIn;
    public bool enemyPositionOnLeft;
    public bool sprint;
    public bool shield;

    public bool willBlock;
    public bool block;

    public bool hitstun;

    public float backDashDistance;
    public float forwardDashDistance;
    public float backDashInitialSpeed;
    public float forwardDashInitialSpeed;
    public float backDashTime;
    public float forwardDashTime;
    public float groundDashDelayTime;

    public bool groundForwardDash;
    public bool groundBackDash;
    public bool airForwardDash;
    public bool airBackDash;
    public float airDashDelayTime;
    public float dashRefreshTime;
    public bool groundDashDelay;

    //Attack
    public bool isAttacking;
    public float DashAttack;
    public float JumpAttack;
    public float SlamAttack;
    //public float ChargeAttack
    public float ComboAttack;
    public float AirBorneAttack;
    public float simpleAttack;


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

        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        foreach (PlayerData playerDatae in game.players) {
            Debug.Log(playerDatae.name);
            Debug.Log(gameObject.name);
            if (playerDatae.name == gameObject.name) {
                playerData = playerDatae;
            }
        }

        // jump = 3f;
        walkSpeed = selectedCharacter.walkSpeed;
        crouchSpeed = 0f;
        sprintMultiplier = selectedCharacter.sprintMultiplier;

        xVelocity = 0f;
        yAccel = selectedCharacter.yAccel;
        maxHeight = selectedCharacter.maxHeight;   

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
        knocked = false;

        willBlock = true;
        block = false;
        hitstun = false;

        dash = false;
        sprint = false;
        shield = false;
        attacking = false;
        isLockedIn = false;

        canDash = true;
        isDashing = false;
        isAirDashing = false;
        hasAirDashed = false;


        backDashInitialSpeed = 50f;
        forwardDashInitialSpeed = 100f;
        backDashDistance = 3f;
        forwardDashDistance = 6f;
        airDashDelayTime = 0.25f;
        groundDashDelayTime = 0.1f;

        groundForwardDash = false;
        groundBackDash = false;
        airForwardDash = false;
        airBackDash = false;
        groundDashDelay = false;

        dashRefreshTime = 0.25f;

      /*  isAttacking = False;
        DashAttack = 10f;
        JumpAttack = 15f;
        SlamAttack = 20f;
        ComboAttack = 30f;
        AirborneAttack = 15f;
        //ChargeAttack = 30f;
*/



    }

    bool getInput(string inputString, string add="") {    // 01234
        if (inputString.Contains("Joy") && !inputString.Contains("stick")) { // Key0XLeft
            string axisName = inputString.Substring(0, 5);
            if (inputString.Split(axisName)[1] == "Left" || inputString.Split(axisName)[1] == "Down") {
                if (Input.GetAxis(axisName) < -0.9) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (Input.GetAxis(axisName) > 0.9) {
                    return true;
                } else {
                    return false;
                }
            }
        } else {
            KeyCode keyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), inputString);
            if (add == "Down") {
                return Input.GetKeyDown(keyCode);
            } else if (add == "Up") {
                return Input.GetKeyUp(keyCode);
            } else {
                return Input.GetKey(keyCode);
            }
        }
    }

    public IEnumerator shakeCamera(float time, float intensity) {
        Transform camera = GameObject.Find("Main Camera").transform;
        Vector3 originalPos = camera.localPosition;
        float changedTime = 0f;

        while (changedTime < time) {
            float offsetX = UnityEngine.Random.Range(-0.5f, 0.5f) * intensity;
            float offsetY = UnityEngine.Random.Range(-0.5f, 0.5f) * intensity;

            camera.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            changedTime += Time.deltaTime;

            yield return null;
        }

        camera.localPosition = originalPos;
    }

    public float getAnimLength(string name) {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        AnimationClip idle = clips[0];
        foreach(AnimationClip clip in clips)
        {
            if (clip.name == "light_forward") {
                idle = clip;
            }
            if (clip.name == name) {
                return clip.length;
            }
        }
        return idle.length;
    }

    void Update(){
        anim.SetInteger("xDirection", xDirection);
        anim.SetBool("crouch", crouch);
        anim.SetBool("sprint", sprint);
        anim.SetBool("onground", onGround);
        anim.SetBool("enemyisonleft", enemyPositionOnLeft);
        anim.SetBool("isdashing", isDashing);
        anim.SetBool("grounddashdelay", groundDashDelay);
        anim.SetBool("isairdashing", isAirDashing);
        anim.SetBool("hasairdashed", hasAirDashed);
        anim.SetBool("attacking", attacking);
        anim.SetBool("isLockedIn", isLockedIn);
        anim.SetFloat("verticalspeed", yVelocity);
        anim.SetInteger("airdirection", airDirection);

        anim.SetBool("up", up);
        anim.SetBool("down", down);

        anim.SetBool("block", block);

        if (opponent.position.x < transform.position.x){
            enemyPositionOnLeft = true;
        } else {
            enemyPositionOnLeft = false;
        }

        if (enemyPositionOnLeft ) {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        } else {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(feet.bounds.center, feet.bounds.size, 0f);
        onGround = false; 
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("ground"))
            {
                onGround = true;
                break; // If you only want to handle the first collider with the tag
            }
        }

        if (onGround == true){
            canMove = true;
        }
        else{
            canMove = false;
        }
        
        
        yVelocity = characterRB.velocity.y;
        initialSpeedY = Mathf.Sqrt(2f * yAccel * maxHeight);
        backDashTime = backDashDistance / backDashInitialSpeed;
        forwardDashTime = forwardDashDistance / forwardDashInitialSpeed;
        string leftCode = playerData.controllerType.left;
        string rightCode = playerData.controllerType.right;
        string upCode = playerData.controllerType.up; //this should be w
        string downCode = playerData.controllerType.down;
        string jumpCode = playerData.controllerType.jump;
        string dashCode = playerData.controllerType.dash;
        string lightCode = playerData.controllerType.light;
        string heavyCode = playerData.controllerType.heavy;
        string specialCode = playerData.controllerType.special;

        // To detect what direction or input the player is doing
        if (getInput(leftCode)) {
            left = true;
        } else {
            left = false;
        }
        
        if (getInput(rightCode)) {
            right = true;
        }
        else{
            right = false;
        }

        if (getInput(upCode)) {
            up = true;
            Debug.Log("Up is true"); 
        } else{
            up = false;
        }

        if (getInput(downCode)) {
            down = true;
        }
        else{
            down = false;
        }
        if (getInput(jumpCode)) {
            jump = true;
        }
        else{
            jump = false;
        }
        if (getInput(jumpCode, "Down")) {
            // Debug Special, reset special
            playerData.special = 0;
        }
        if (getInput(dashCode, "Down")) {
            dash = true;
        }
        else{
            dash = false;
        }
        if (getInput(dashCode) && xDirection != 0){
            sprint = true;
            shield = false;
        }
        else{
            sprint = false;
            shield = false;
        }

        if (dash == false && sprint == false && attacking == false && left == false && right == false) {
            willBlock = true;
        } else {
            willBlock = false;
        }
        
        if (getInput(lightCode, "Down") && (attacking == false) && (isLockedIn == false)) { // be sure to clear
            string animSuffix = onGround ? "" : "_air";
            if (up && onGround) { // it doesnt work for gree chara because we havent defiend lUp for falfafl, only andre in globalcotnrller(Works no)
                Debug.Log("GOING UP");// it seems that up isnt working
                attacking = true;
                Invoke("refreshAttackCooldown", getAnimLength("light_up"));
                selectedCharacter.lUp(anim, gameObject, enemyPositionOnLeft ? -1 : 1);// test lets have it debug .log 
                anim.SetTrigger("light");
                // Shake camera a little
                //StartCoroutine(shakeCamera(0.2f, 0.2f)); // 1 second, 1 intensity

            } if (down) {
                if (onGround) {
                    selectedCharacter.lDown(anim, gameObject, enemyPositionOnLeft ? -1 : 1);
                } else {
                    selectedCharacter.laDown(anim, gameObject, enemyPositionOnLeft ? -1 : 1);// test lets have it debug .log 
                }
                anim.SetTrigger("light");
                attacking = true;
                Invoke("refreshAttackCooldown", getAnimLength("light_down" + animSuffix));
                // Shake camera a little
                //StartCoroutine(shakeCamera(0.2f, 0.2f)); // 1 second, 1 intensity
            } else if (enemyPositionOnLeft) {
                Debug.Log("eps");
                // no aman bruh just hold W(up for orange) and attack its not wroking, weird
                if (right && onGround) {
                    selectedCharacter.lBackward(anim, gameObject, -1);
                    anim.SetBool("forward", false);
                    attacking = true;
                    Invoke("refreshAttackCooldown", getAnimLength("light_back"));
                    anim.SetTrigger("light");
                    Debug.Log("light_back" + animSuffix);
                    Debug.Log(getAnimLength("light_back" + animSuffix));
                } else {
                    if (onGround) {
                        selectedCharacter.lForward(anim, gameObject, -1);
                    } else {
                        selectedCharacter.laForward(anim, gameObject, -1);
                    }
                    anim.SetBool("forward", true);
                    attacking = true;
                    Invoke("refreshAttackCooldown", getAnimLength("light_forward" + animSuffix));
                    anim.SetTrigger("light");
                    Debug.Log("light_forward" + animSuffix);
                    Debug.Log(getAnimLength("light_forward" + animSuffix));
                }
                 // spelled wrong
                
            } else if (enemyPositionOnLeft == false) {
                if (left && onGround) {
                    selectedCharacter.lBackward(anim, gameObject, 1);
                    anim.SetBool("forward", false);
                    attacking = true;
                    Invoke("refreshAttackCooldown", getAnimLength("light_back"));
                    anim.SetTrigger("light");
                    Debug.Log("light_forward" + animSuffix);
                    Debug.Log(getAnimLength("light_forward" + animSuffix));
                } else {
                    if (onGround) {
                        selectedCharacter.lForward(anim, gameObject, 1);
                    } else {
                        selectedCharacter.laForward(anim, gameObject, 1);
                    }
                    anim.SetBool("forward", true);
                    attacking = true;
                    Invoke("refreshAttackCooldown", getAnimLength("light_forward" + animSuffix));
                    anim.SetTrigger("light");
                    Debug.Log("light_forward" + animSuffix);
                    Debug.Log(getAnimLength("light_forward" + animSuffix));
                }
                // lets go to globalcotrooller
                //selectedCharacter.lBackward(anim, charaObj); 
            }
            
            // we have a variable or that
            // if the enemyposition is on left and left == true, the we are attacking forward
            // 
            
        }

        if (getInput(heavyCode, "Down") && (attacking == false) && (isLockedIn == false)) { // be sure to clear
            string animSuffix = onGround ? "" : "_air";
            if (up && onGround) { // it doesnt work for gree chara because we havent defiend lUp for falfafl, only andre in globalcotnrller(Works no)
                selectedCharacter.hUp(anim, gameObject, enemyPositionOnLeft ? -1 : 1);// test lets have it debug .log 
                Debug.Log("GOING UP");// it seems that up isnt working
                anim.SetTrigger("heavy");
                attacking = true;
                Invoke("refreshAttackCooldown", getAnimLength("heavy_up"));
                Debug.Log("GOING UP");// it seems that up isnt working
                // Shake camera a little
                //StartCoroutine(shakeCamera(0.2f, 0.2f)); // 1 second, 1 intensity
            } else if (down) { // it doesnt work for gree chara because we havent defiend lUp for falfafl, only andre in globalcotnrller(Works no)
                if (onGround) {
                    selectedCharacter.hDown(anim, gameObject, enemyPositionOnLeft ? -1 : 1);// test lets have it debug .log 
                } else {
                    selectedCharacter.haDown(anim, gameObject, enemyPositionOnLeft ? -1 : 1);
                }
                Debug.Log("GOING UP");// it seems that up isnt working
                anim.SetTrigger("heavy");
                attacking = true;
                Invoke("refreshAttackCooldown", getAnimLength("heavy_down" + animSuffix));
                Debug.Log("GOING UP");// it seems that up isnt working
                // Shake camera a little
                //StartCoroutine(shakeCamera(0.2f, 0.2f)); // 1 second, 1 intensity
            } else if ((right||left) && enemyPositionOnLeft) {
                // no aman bruh just hold W(up for orange) and attack its not wroking, weird
                if (right && onGround) {
                    selectedCharacter.hBackward(anim, gameObject, -1);
                    anim.SetBool("forward", false);
                    attacking = true;
                    Invoke("refreshAttackCooldown", getAnimLength("heavy_back"));
                    anim.SetTrigger("heavy");
                } else if (left) {
                    if (onGround) {
                        selectedCharacter.hForward(anim, gameObject, -1);
                    } else {
                        selectedCharacter.haForward(anim, gameObject, -1);
                    }
                    anim.SetBool("forward", true);
                    attacking = true;
                    Invoke("refreshAttackCooldown", getAnimLength("heavy_forward" + animSuffix));
                    anim.SetTrigger("heavy");
                }
                 // spelled wrong
                
            } else if ((right||left) && enemyPositionOnLeft == false) {
                if (left && onGround) {
                    selectedCharacter.hBackward(anim, gameObject, 1);
                    anim.SetBool("forward", false);
                    attacking = true;
                    Invoke("refreshAttackCooldown", getAnimLength("heavy_back"));
                    anim.SetTrigger("heavy");
                } else if (right) {
                    if (onGround) {
                        selectedCharacter.hForward(anim, gameObject, 1);
                    } else {
                        selectedCharacter.haForward(anim, gameObject, 1);
                    }
                    anim.SetBool("forward", true);
                    attacking = true;
                    Invoke("refreshAttackCooldown", getAnimLength("heavy_forward" + animSuffix));
                    anim.SetTrigger("heavy");
                }
                // lets go to globalcotrooller
                //selectedCharacter.lBackward(anim, charaObj); 
            }
            
            // we have a variable or that
            // if the enemyposition is on left and left == true, the we are attacking forward
            // 
            
        }

        //nvm

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

        if (up == false && getInput(downCode, "Down") && onGround) {
            // Special Debug, when crouching special will go up
            playerData.special += 33.33f;
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
            jumpFunction();//right here
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
        else if (getInput(dashCode) && xDirection == 0){
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
        else if (onGround == true){
            isAirDashing = false;
        }
        if (onGround == true && groundBackDash == false){
            refreshMoveCooldown();
        }

        //Attack

        




        
    }
    void FixedUpdate()
    {
   
        if (knocked) {
            
        } else if (onGround && block) {
            characterRB.velocity = new Vector2(0, 0);
            characterRB.gravityScale = 0f;
            
        } else if (onGround && attacking) {
            characterRB.velocity = new Vector2(0, 0);
            characterRB.gravityScale = 0f;
        } else if (onGround == false && block) {
            characterRB.velocity = new Vector2(airDirection * walkSpeed, characterRB.velocity.y); 
            characterRB.gravityScale = yAccel;
        } else if (onGround == false && attacking) {
            characterRB.velocity = new Vector2(airDirection * walkSpeed, characterRB.velocity.y); 
            characterRB.gravityScale = yAccel;
        } else if (isDashing == false && canMove == true){
            characterRB.gravityScale = yAccel;
            characterRB.velocity = new Vector2(xDirection * xVelocity, characterRB.velocity.y);
        }
        else if (isDashing == false && canMove == false){
            characterRB.gravityScale = yAccel;
            characterRB.velocity = new Vector2(airDirection * walkSpeed, characterRB.velocity.y); 
        }
        else {
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



        
        //animation
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is tagged as ground
        if (other.gameObject.name.Contains("Hit"))
        {
            int attackAmount = Int32.Parse(other.gameObject.name.Split("Hit")[1].Split("Knock")[0]);
            // we need to change this code to 
            // say we name every hit thing "player1" + "Hit" + "amount"
            if (other.gameObject.name.Split("Hit")[0] != playerData.name) {
                PlayerData otherPData = GameObject.Find(other.gameObject.name.Split("Hit")[0]).GetComponent<PlayerController>().playerData;
                // oh ok, the problem is that falfafel doesnt have code lets add
                Debug.Log(other.gameObject.name);
                Debug.Log(other.gameObject.name.Split("Hit")[0] );
                Debug.Log(playerData.name);
                Debug.Log(attackAmount);
                if (willBlock) {
                    if (other.gameObject.name.Split("Knock")[1] != "") {
                        string knockString = other.gameObject.name.Split("Knock")[1].Split("PWR")[0];
                        Debug.Log(knockString);
                        int knockAmountX = Int32.Parse(knockString.Split("X")[1].Split("Y")[0]);
                        Debug.Log(knockAmountX);
                        int knockAmountY = Int32.Parse(knockString.Split("Y")[1].Split("T")[0]);
                        Debug.Log(knockAmountY);
                        Debug.Log(other.gameObject.name);
                        Debug.Log(other.gameObject.name.Split("PWR")[1]);
                        float knockTime = float.Parse(knockString.Split("T")[1]);
                        
                        string ATKString = other.gameObject.name.Split("PWR")[1];
                        if (ATKString != "") {
                            if (ATKString == "0") {
                                block = true;
                                otherPData.special += 5;
                            } if (ATKString == "1") {
                                block = false;
                                selectedCharacter.hitGround(anim, gameObject, playerData, attackAmount);
                                knocked = true;
                                otherPData.special += 10;
                                Invoke("refreshKnockCooldown", knockTime);
                                characterRB.velocity = new Vector2(knockAmountX, knockAmountY);
                            }
                        } else {
                            block = true;
                        }
                        // Apply force
                        
                    }
                } else {
                    selectedCharacter.hitGround(anim, gameObject, playerData, attackAmount);   
                    // Shake camera a LOT
                    //StartCoroutine(shakeCamera(0.5f, 0.5f)); // 1 second, 1 intensity
                    if (other.gameObject.name.Split("Knock")[1] != "") {
                        string knockString = other.gameObject.name.Split("Knock")[1].Split("PWR")[0];
                        Debug.Log(knockString);
                        int knockAmountX = Int32.Parse(knockString.Split("X")[1].Split("Y")[0]);
                        Debug.Log(knockAmountX);
                        int knockAmountY = Int32.Parse(knockString.Split("Y")[1].Split("T")[0]);
                        Debug.Log(knockAmountY);
                        float knockTime = float.Parse(knockString.Split("T")[1]);
                        string ATKString = other.gameObject.name.Split("PWR")[1];
                        if (ATKString != "") {
                            if (ATKString == "0") {
                                otherPData.special += 5;
                            } if (ATKString == "1") {
                                otherPData.special += 10;
                            }
                        }
                        // Apply force
                        knocked = true;
                        Invoke("refreshKnockCooldown", knockTime);
                        characterRB.velocity = new Vector2(knockAmountX, knockAmountY);
                    }
                }
            }

        }
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider is tagged as ground
        if (other.gameObject.name.Contains("Hit"))
        {
            int attackAmount = Int32.Parse(other.gameObject.name.Split("Hit")[1].Split("Knock")[0]);
            // we need to change this code to 
            // say we name every hit thing "player1" + "Hit" + "amount"
            if (other.gameObject.name.Split("Hit")[0] != playerData.name) {
                // oh ok, the problem is that falfafel doesnt have code lets add
                Debug.Log(other.gameObject.name);
                Debug.Log(other.gameObject.name.Split("Hit")[0] );
                Debug.Log(playerData.name);
                Debug.Log(attackAmount);
                if (willBlock) {
                    Invoke("refreshBlock", 1f); // 
                }
            }

        }
        
    }
    // Foot Collider
    /**public void feet.OnTriggerEnter2D(Collider2D ground)
    {
        // Check if the collider is tagged as ground
        if (ground.CompareTag("ground"))
        {
            onGround = true; 
            canMove = true;
        }
        
    }

    public void feet.OnTriggerExit2D(Collider2D ground)
    {
        // Check if the collider is tagged as ground
        if (ground.CompareTag("ground"))
        {
            onGround = false;
            canMove = false;
        }
    }*/
    public void jumpBuffer(){
        onGround = true; 
    }
    public void jumpFunction(){
        airDirection = xDirection;
        characterRB.velocity = new Vector2(characterRB.velocity.x, initialSpeedY);
    }
    public void dashFunction(){
        canDash = false;
        isDashing = true;
        if (xDirection == -1 && onGround == true && enemyPositionOnLeft == true){
            canDash = true;
            isDashing = false;

        }
        else if (xDirection == 1 && onGround == true && enemyPositionOnLeft == false){
            canDash = true;
            isDashing = false;

        }
        else if (xDirection == 1 && onGround == true && enemyPositionOnLeft == true){
            //backward ground
            dashDirection = 1;
            groundDashDelay = true;
            Invoke("groundDashStart", groundDashDelayTime);
        }
        else if (xDirection == -1 && onGround == true && enemyPositionOnLeft == false){
            //backward ground
            dashDirection = -1;
            groundDashDelay = true;
            Invoke("groundDashStart", groundDashDelayTime);
            

        }
        else if (xDirection == -1 && onGround == false && enemyPositionOnLeft == true){
            canDash = true;
            isDashing = false;

        }
        else if (xDirection == 1 && onGround == false && enemyPositionOnLeft == false){
            canDash = true;
            isDashing = false;

        }
        else if (xDirection == 1 && onGround == false && enemyPositionOnLeft == true){
            //backward air
            dashDirection = 1;
            airDirection = 1;
            airBackDash = true;

        }
        else if (xDirection == -1 && onGround == false && enemyPositionOnLeft == false){
            //backward air
            dashDirection = -1;
            airDirection = -1;
            airBackDash = true;

        }
        
        

    }
    public void stopDash(){
        
        if (groundBackDash == true){
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
    public void groundDashStart(){
        groundBackDash = true;
        groundDashDelay = false;
    }
    public void refreshKnockCooldown() {
        knocked = false;
    }
    public void refreshAttackCooldown() {
        attacking = false;
    }
    public void refreshBlock() {
        block = false;
    }
}
        