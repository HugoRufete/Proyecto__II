using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comprar_O_Negociar : MonoBehaviour
{
    public GameObject Comprar;
    public GameObject Negociar;
   
    public GameObject NegociarOComprar;
   
    public void ActivarNegociar()
    {
        NegociarOComprar.SetActive(true);
    }

    public void DesactivarNegociarOComprar()
    {
        NegociarOComprar.SetActive(false);
    }
}
