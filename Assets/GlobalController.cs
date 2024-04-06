using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;

using static PlayerData;
using static CharacterBase;

public class GlobalController : MonoBehaviour
{
    public CharacterBase Andre;
    public CharacterBase FLLFFL;
    public CharacterBase Dante;

    // controllerTypes
    public ControllerType wasd;

    public ControllerType arrow;
    // exPlayer {name: "default", "controllerType": wasd}
    public List<PlayerData> players;
    // exMap "default"
    public string map;
    // exMute false
    public string playType;
    public bool mute;
    // exFullScreen false
    public bool fullScreen;
    public bool paused;
    public int num;


    // To keep it loaded thru scenes
    void Awake() 
    {
        if (gameObject.name == "GLOBALOBJECT") {
            DontDestroyOnLoad(transform.gameObject);
            Debug.Log("Using GLOBALOBJECT");
        } else {
            Debug.Log("Using non GAMEOBJECT");
        }
    }

    void SetupCharacters() {
        GameObject hitBox = Resources.Load<GameObject>("Hit");
        
        // Andre code
        Andre = new CharacterBase();
        Andre.walkSpeed = 4.5f;
        Andre.sprintMultiplier = 2.7f;
        Andre.maxHeight = 80f;
        Andre.yAccel = 9f;
        Andre.lightAttackUpValue = 5;
        Andre.lightAttackDownValue = 5;
        Andre.lightAttackForwardValue = 5;
        Andre.lightAttackBackValue = 5;
        // heavy
        Andre.heavyAttackUpValue = 5;
        Andre.heavyAttackDownValue = 5;
        Andre.heavyAttackForwardValue = 20;
        Andre.heavyAttackBackValue = 5;
        // air light
        Andre.lightAttackForwardAirValue = 5;
        Andre.lightAttackDownAirValue = 5;
        // air heavy
        Andre.heavyAttackForwardAirValue = 5;
        Andre.heavyAttackDownAirValue = 5;
        Andre.Attack = () => {


            
           // just make an empty prefab with boxcollider then you just instansiate, change position and collider bounds as you desire. also screensare so i can see.
           // ill chat 
            Debug.Log("Andre has attacked");
        };
        Andre.vJumpUp = (Animator anim, GameObject charaObj) => {
            Debug.Log("Andre has Jumped");
            // run jump animation I havent impoerted the jump animation this is just fo
// here lets get fflfl the vjumpup
        };
        Andre.hitGround = (Animator anim, GameObject charaObj, PlayerData playerData, int attackAmount) => {
            // run hitground animation
            // move charaObj backwards
            Debug.Log(playerData.health);
            playerData.health -= attackAmount;
            Debug.Log(playerData.health);
        };

        Andre.lForward = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + Andre.lightAttackForwardValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "0";// + "Anim"; // i fixed 
            Debug.Log("lForward from " + charaObj.name);
            
        };
        Andre.lBackward = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + Andre.lightAttackBackValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR"; // 
            Debug.Log("lBackward from " + charaObj.name);
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        };
        Andre.lUp = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + Andre.lightAttackUpValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR"; // 
            Debug.Log("lUp from " + charaObj.name);
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        }; // lets got to playercontroller
        Andre.lDown = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + Andre.lightAttackDownValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR"; // 
            Debug.Log("lDown from " + charaObj.name);
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        }; // lets got to playercontroller

        Andre.hForward = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + Andre.heavyAttackForwardValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "1";; // i fixed 
            Debug.Log("hForward from " + charaObj.name);
            charaObj.transform.GetChild(2).name = charaObj.name + "Hit" + Andre.heavyAttackForwardValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "1";; // i fixed 
            Debug.Log("hForward from " + charaObj.name);
            charaObj.transform.GetChild(3).name = charaObj.name + "Hit" + Andre.heavyAttackForwardValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "1";; // i fixed 
            Debug.Log("hForward from " + charaObj.name);
            
        };
        Andre.hBackward = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + Andre.heavyAttackBackValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "1";; // i fixed 
            Debug.Log("hForward from " + charaObj.name);
            
        };
        Andre.hUp = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + Andre.heavyAttackUpValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "1";; // i fixed 
            Debug.Log("hForward from " + charaObj.name);
            
        };
        Andre.hDown = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + Andre.heavyAttackDownValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "1";; // i fixed 
            Debug.Log("hForward from " + charaObj.name);
            
        };
        // lair
        Andre.laForward = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            //charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + Andre.lightAttackForwardAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            //Debug.Log("hForward from " + charaObj.name);
            
        };
        Andre.laDown = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + Andre.lightAttackDownAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            Debug.Log("laDown from " + charaObj.name);
            
        };
        // hair
        Andre.haForward = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            //charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + Andre.heavyAttackForwardAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            //Debug.Log("haForward from " + charaObj.name);
            
        };
        Andre.haDown = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            //charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + Andre.heavyAttackDownAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            //Debug.Log("haDown from " + charaObj.name);
            
        };
        // FLLFFL code
        FLLFFL = new CharacterBase();
        FLLFFL.walkSpeed = 7.5f;
        FLLFFL.sprintMultiplier = 3f;
        FLLFFL.yAccel = 6f;
        FLLFFL.maxHeight = 100f;
        FLLFFL.lightAttackUpValue = 5;
        FLLFFL.lightAttackDownValue = 5;
        FLLFFL.lightAttackForwardValue = 5;
        FLLFFL.lightAttackBackValue = 5;
        // air light
        FLLFFL.lightAttackForwardAirValue = 5;
        FLLFFL.lightAttackDownAirValue = 5;
        // air heavy
        FLLFFL.heavyAttackForwardAirValue = 5;
        FLLFFL.heavyAttackDownAirValue = 5;
        FLLFFL.Attack = () => {
            Debug.Log("FLLFFL has attacked");
        };
        FLLFFL.hitGround = (Animator anim, GameObject charaObj, PlayerData playerData, int attackAmount) => {
            // run hitground animation
            // move charaObj backwards
            Debug.Log(playerData.health);
            playerData.health -= attackAmount;
            // basically when you are standing still or moving towards opponent and attack, it runs lforward, when you move away from opponent and attack it runs lbackward
            Debug.Log(playerData.health);
        };

        FLLFFL.lForward = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + FLLFFL.lightAttackForwardValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "0";;// + "Anim"; // i fixed 
            Debug.Log("lForward from " + charaObj.name);
            
        };
        FLLFFL.lBackward = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + FLLFFL.lightAttackBackValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "0";;// + "Anim"; // i fixed 
            Debug.Log("lBackward from " + charaObj.name);
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // it runs when u press up and attack
        };
        FLLFFL.lUp = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + FLLFFL.lightAttackUpValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "0";;// + "Anim"; // i fixed 
            Debug.Log("lUp from " + charaObj.name);
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        }; // lets got to playercontroller
        FLLFFL.lDown = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + FLLFFL.lightAttackDownValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "0";;// + "Anim"; // i fixed 
            Debug.Log("lDown from " + charaObj.name);
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        }; // lets got to playercontroller

        FLLFFL.hForward = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + FLLFFL.heavyAttackForwardValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "1";;// + "Anim"; // i fixed 
            Debug.Log("hForward from " + charaObj.name);
            
        };
        FLLFFL.hBackward = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + FLLFFL.heavyAttackBackValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "1";;// + "Anim"; // i fixed 
            Debug.Log("hBackward from " + charaObj.name);
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // it runs when u press up and attack
        };
        FLLFFL.hUp = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack1 = charaObj.transform.GetChild(1).gameObject;
            attack1.name = charaObj.name + "Hit" + (FLLFFL.heavyAttackUpValue/2).ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "1";;// + "Anim"; // i fixed 
            GameObject attack2 = charaObj.transform.GetChild(2).gameObject;
            attack2.name = charaObj.name + "Hit" + (FLLFFL.heavyAttackUpValue).ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "1";;// + "Anim"; // i fixed 
            Debug.Log("hUp from " + charaObj.name);
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        }; // lets got to playercontroller
        FLLFFL.hDown = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = charaObj.transform.GetChild(1).gameObject;
            attack.name = charaObj.name + "Hit" + FLLFFL.heavyAttackDownValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "1";;// + "Anim"; // i fixed 
            Debug.Log("hDown from " + charaObj.name);
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        }; // lets got to playercontroller
        // lair
        FLLFFL.laForward = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            //charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + FLLFFL.lightAttackForwardAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            //Debug.Log("laForward from " + charaObj.name);
            
        };
        FLLFFL.laDown = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + FLLFFL.lightAttackForwardAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            Debug.Log("laDown from " + charaObj.name);
            charaObj.transform.GetChild(2).name = charaObj.name + "Hit" + FLLFFL.lightAttackForwardAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            Debug.Log("laDown from " + charaObj.name);
            charaObj.transform.GetChild(3).name = charaObj.name + "Hit" + FLLFFL.lightAttackForwardAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (0).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            Debug.Log("laDown from " + charaObj.name);
            
        };
        // hair
        FLLFFL.haForward = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            //charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + FLLFFL.heavyAttackForwardAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            //Debug.Log("haForward from " + charaObj.name);
            
        };
        FLLFFL.haDown = (Animator anim, GameObject charaObj, int dir) => { // this is how we get chara position
            //charaObj.transform.GetChild(1).gameObject.name = charaObj.name + "Hit" + FLLFFL.heavyAttackDownAirValue.ToString() + "Knock" + "X" + (10*(dir)).ToString() + "Y" + (10).ToString() + "T" + "0.8" + "PWR" + "0";; // i fixed 
            //Debug.Log("haDown from " + charaObj.name);
            
        };
        // other
        FLLFFL.vJumpUp = (Animator anim, GameObject charaObj) => {
            Debug.Log("FLLFFL has Jumped"); // test adn look in console make sure that it only runs if you arent moving and just jump in place
        };// wait i think we need to add this in playercontroller
        // Dante code
        Dante = new CharacterBase();
        Dante.walkSpeed = 5f;
        Dante.sprintMultiplier = 5f;
        Dante.yAccel = 5f;
        Dante.maxHeight = 5f;
        Dante.Attack = () => {
            Debug.Log("Dante has attacked");
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupCharacters();

        wasd = new ControllerType("W", "S", "A", "D", "Space", "LeftShift", "Mouse0", "F", "R"); // 
        arrow = new ControllerType("UpArrow", "DownArrow", "LeftArrow", "RightArrow", "RightControl", "RightShift", "Mouse1", "T", "P");
        players = new List<PlayerData>();
        map = "";
        playType = "";
        mute = false;
        fullScreen = false;

        paused = false;
    }

    public void printData() {
        int index = 1;
        foreach (PlayerData player in players) {
            Debug.Log("PLAYERS:" + "\n" +
                      "player" + index.ToString() + "\n" + 
                      "     " + player.name + "\n" +
                      "     " + player.character + "\n" +
                      "     " + player.controllerType + "\n" +
                      "MAP: " + map + "\n" + 
                      "MUTE: " + mute.ToString() + "\n" + 
                      "FULLSCREEN: " + fullScreen.ToString());
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
