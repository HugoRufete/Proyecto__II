using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudito : MonoBehaviour
{

    public Transform Player;
    public float velocidadMovimiento = 5.0f;
    Animator myanimator;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        myanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.position - transform.position;
        direction.Normalize();
       
        transform.Translate(direction * velocidadMovimiento * Time.deltaTime);

        if (direction.magnitude > 0)
        {
            myanimator.SetBool("IsWalking", true);
        }

        else
        {
            myanimator.SetBool("IsWalking", false);
        }

       if (transform.position.x > Player.position.x)
       {
            transform.eulerAngles = new Vector3(0, 180, 0);
       }

       else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
