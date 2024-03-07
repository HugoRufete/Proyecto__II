using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    //Variables de movimiento
    public float movSpeed;
    float SpeedX, SpeedY;

    //Hacia donde va a mirar el personaje
    private Vector3 objective;
    [SerializeField] private new Camera camera;


    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        Movement();
    }

    //Movimiento simple de personaje + rotacion de este a traves del cursor
    void Movement()
    {
        //Cogemos las inputs y las multiplicamos por la velocidad
        SpeedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        SpeedY = Input.GetAxisRaw("Vertical") * movSpeed;

        //Le aplicamos la velocidad al RB
        rb.velocity = new Vector2(SpeedX, SpeedY);

        /*//Indicamos donde se encuentra el objetivo al que queremos mirar
        objective = camera.ScreenToWorldPoint(Input.mousePosition);
        float radAngle = Mathf.Atan2(objective.y - transform.position.y, objective.x - transform.position.x);
        float degAngle = (180 / Mathf.PI) * radAngle - 90;

        //Cambiamos la rotacion del personaje dependiendo de donde esta el objetivo
        if (objective.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (objective.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //transform.rotation = Quaternion.Euler(0, 0, degAngle);

*/
    }
}
