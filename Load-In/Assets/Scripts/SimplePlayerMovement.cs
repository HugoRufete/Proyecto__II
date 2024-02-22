using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{

    public float movSpeed;
    float SpeedX, SpeedY;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        SpeedX = Input.GetAxisRaw("Horizontal") * movSpeed;        
        SpeedY = Input.GetAxisRaw("Vertical") * movSpeed;        

        rb.velocity = new Vector2 (SpeedX, SpeedY);
    }
}
