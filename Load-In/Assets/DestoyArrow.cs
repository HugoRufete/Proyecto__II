using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyArrow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag ("Fragmento"))
        {
            Destroy (gameObject);
        }
    }
}
