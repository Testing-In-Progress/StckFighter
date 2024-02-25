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
        
        // Andre code
        Andre = new CharacterBase();
        Andre.minJumpHeight = 3;
        Andre.maxJumpHeight = 5;
        Andre.movementSpeed = 3;
        Andre.fallSpeed = 5;
        Andre.jumpSpeed = 0.05f;
        Andre.Attack = () => {
            Debug.Log("Andre has attacked");
        };

        // FLLFFL code
        FLLFFL = new CharacterBase();
        FLLFFL.minJumpHeight = 3;
        FLLFFL.maxJumpHeight = 5;
        FLLFFL.movementSpeed = 3;
        FLLFFL.fallSpeed = 5;
        FLLFFL.jumpSpeed = 0.07f;
        FLLFFL.Attack = () => {
            Debug.Log("FLLFFL has attacked");
        };

        // Dante code
        Dante = new CharacterBase();
        Dante.minJumpHeight = 3;
        Dante.maxJumpHeight = 5;
        Dante.movementSpeed = 3;
        Dante.fallSpeed = 5;
        Dante.jumpSpeed = 0.07f;
        Dante.Attack = () => {
            Debug.Log("Dante has attacked");
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupCharacters();

        wasd = new ControllerType("W", "S", "A", "D", "Space", "LeftShift", "Mouse0");
        arrow = new ControllerType("UpArrow", "DownArrow", "LeftArrow", "RightArrow", "RightControl", "RightShift", "Mouse1");
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
