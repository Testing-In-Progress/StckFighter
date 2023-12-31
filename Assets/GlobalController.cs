using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;

using static PlayerData;


public class GlobalController : MonoBehaviour
{
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
        if (gameObject.name == "GAMEOBJECT") {
            DontDestroyOnLoad(transform.gameObject);
            Debug.Log("Using GAMEOBJECT");
        } else {
            Debug.Log("Using non GAMEOBJECT");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        wasd = new ControllerType("W", "A", "S", "D", "Space", "LeftShift", "Mouse0");
        arrow = new ControllerType("LeftArrow", "UpArrow", "LeftArrow", "RightArrow", "RightControl", "RightShift", "Mouse1");
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
