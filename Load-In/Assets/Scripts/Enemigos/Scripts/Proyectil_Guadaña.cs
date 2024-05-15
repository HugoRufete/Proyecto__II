using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_Guadaña : MonoBehaviour
{
    public AudioClip soniditoPro;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si el tag de la colision es Player se llama al metodo de arriba
        if (other.tag == ("Player"))
        {
            ControladorSonido.Instance.EjecutarSonido(soniditoPro);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}


