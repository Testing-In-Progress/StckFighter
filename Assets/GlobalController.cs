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

    // To keep it loaded thru scenes
    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        wasd = new ControllerType("W", "A", "S", "D", "Space", "LeftShift", "Mouse0");
        arrow = new ControllerType("LeftArrow", "UpArrow", "LeftArrow", "RightArrow", "RightControl", "RightShift", "Mouse1");
        players = new List<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
