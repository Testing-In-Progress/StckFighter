using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GlobalController;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalController game;
    
    public static int playNum;
    
    [SerializeField] float jump;
    [SerializeField] float speed;

    [SerializeField] bool Keys;
    void Start()
    {
        game = GameObject.Find("GLOBALOBJECT").GetComponent<GlobalController>();
        game.printData();

        jump = 3f;
        speed = 8f;
    }
    // Update is called once per frame
    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement);

    }
}
        