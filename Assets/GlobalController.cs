using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    // controllerTypes
    public wasd = {
        up: "w",
        down: "s",
        left: "a",
        right: "d",
        jump: "space",
        dash: "LShift"
        attack: "LClick"
    }

    public arrow = {
        up: "Up",
        down: "Down",
        left: "Left",
        right: "Right",
        jump: "RControl",
        dash: "RShift",
        attack: "RCLick"
    }
    // exPlayer {name: "geo", stickmanName: "default", "controllerType": wasd}
    public players = {};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
