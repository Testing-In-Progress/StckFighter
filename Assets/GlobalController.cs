using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;


public class GlobalController : MonoBehaviour
{
    // controllerTypes
    public ControllerType wasd;

    public ControllerType arrow;
    // exPlayer {stickmanName: "default", "controllerType": wasd}
    public dynamic players;

    // Start is called before the first frame update
    void Start()
    {
        wasd = new ControllerType("w", "s", "a", "d", "space", "LShift", "LClick");
        arrow = new ControllerType("up", "down", "left", "right", "rcontrol", "rshift", "rClick");
        players = new ExpandoObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
