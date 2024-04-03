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
            GameObject attack = Instantiate(hitBox);
            attack.name = charaObj.name + "Hit" + Andre.lightAttackForwardValue.ToString(); // i fixed 
            int attackDistanceFromPlayer = 2;// were gonna define in this function
            attack.transform.localPosition = new Vector2(charaObj.transform.position.x + attackDistanceFromPlayer*dir, charaObj.transform.position.y); 
            attack.transform.parent = charaObj.transform;
            Debug.Log("lForward from " + charaObj.name);
            Destroy(attack, 0.25f); //  fixed, run
            
        };

        Andre.lBackward = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = Instantiate(hitBox);
            attack.name = charaObj.name + "Hit" + Andre.lightAttackBackValue.ToString(); // 
            int attackDistanceFromPlayer = 2;// were gonna define in this function
            attack.transform.localPosition = new Vector2(charaObj.transform.position.x + attackDistanceFromPlayer*dir*-1, charaObj.transform.position.y); 
            attack.transform.parent = charaObj.transform;
            Debug.Log("lBackward from " + charaObj.name);
            Destroy(attack, 0.25f); //  go to characterbase
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        };
        Andre.lUp = (Animator anim, GameObject charaObj) => { // 
            GameObject attack = Instantiate(hitBox);
            attack.name = charaObj.name + "Hit" + Andre.lightAttackUpValue.ToString(); // 
            int attackDistanceFromPlayer = 4;// now i think we should add the 
            attack.transform.localPosition = new Vector2(charaObj.transform.position.x, charaObj.transform.position.y+attackDistanceFromPlayer); 
            attack.transform.parent = charaObj.transform;
            Debug.Log("lBackward from " + charaObj.name);
            Destroy(attack, 0.25f); //  go to characterbase
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        }; // lets got to playercontroller
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
            GameObject attack = Instantiate(hitBox);
            attack.name = charaObj.name + "Hit" + FLLFFL.lightAttackForwardValue.ToString(); // i fixed 
            int attackDistanceFromPlayer = 2;// were gonna define in this function
            attack.transform.localPosition = new Vector2(charaObj.transform.position.x + attackDistanceFromPlayer*dir, charaObj.transform.position.y); 
            attack.transform.parent = charaObj.transform;
            Debug.Log("lForward from " + charaObj.name);
            Destroy(attack, 0.7f); //  fixed, run
            
        };

        FLLFFL.lBackward = (Animator anim, GameObject charaObj, int dir) => { // 
            GameObject attack = Instantiate(hitBox);
            attack.name = charaObj.name + "Hit" + FLLFFL.lightAttackBackValue.ToString(); // 
            int attackDistanceFromPlayer = 2;// were gonna define in this function
            attack.transform.localPosition = new Vector2(charaObj.transform.position.x + attackDistanceFromPlayer*dir*-1, charaObj.transform.position.y); 
            attack.transform.parent = charaObj.transform;
            Debug.Log("lBackward from " + charaObj.name);
            Destroy(attack, 2); //  go to characterbase
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // it runs when u press up and attack
        };
        FLLFFL.lUp = (Animator anim, GameObject charaObj) => { // 
            GameObject attack = Instantiate(hitBox);
            attack.name = charaObj.name + "Hit" + FLLFFL.lightAttackUpValue.ToString(); // 
            int attackDistanceFromPlayer = 4;// were gonna define in this function
            attack.transform.localPosition = new Vector2(charaObj.transform.position.x, charaObj.transform.position.y+attackDistanceFromPlayer); 
            attack.transform.parent = charaObj.transform;
            Debug.Log("lBackward from " + charaObj.name);
            Destroy(attack, 2); //  go to characterbase
            // aman i changed the bool to an int so we can just multiply to change te direction of attack
            // ok 
        }; // lets got to playercontroller
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
