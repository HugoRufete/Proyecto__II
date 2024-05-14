using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AmmoBox : MonoBehaviour
{
    private NumMun numMun;
    private Animator anim;

    private void Start()
    {
        numMun = GameObject.Find("HUD_Munición").GetComponent<NumMun>();
        GameObject targetObject = GameObject.FindWithTag("PopUps");
        anim = targetObject.GetComponent<Animator>();

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            numMun.NumCajas++;
            Destroy(gameObject);
        }
    }
}