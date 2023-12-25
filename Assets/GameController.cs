using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GlobalController;

public class GameController : MonoBehaviour
{
    public GlobalController game;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();
        // Instansiate the Game
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("space key was pressed");
        }
    }


}
