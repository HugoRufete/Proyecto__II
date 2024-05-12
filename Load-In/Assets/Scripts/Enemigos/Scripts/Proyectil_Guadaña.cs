using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_Guadaña : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el tag de la colision es Player se llama al metodo de arriba
        if (collision.tag == ("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}


