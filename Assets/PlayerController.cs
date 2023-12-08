using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    public static int playNum;
    
    [SerializeField] float jump;
    [SerializeField] float speed;

    [SerializeField] bool Keys;
    void Start()
    {
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
        