using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecogerPuntos : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject efect; 

    //Hará que el jugador pueda recoger e interactuar con las "monedas" del juego y que se sumen  puntos mediante un trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            Instantiate(efect, transform.position, Quaternion.identity);
            Destroy(gameObject);//Destruimos el objeto para que se de por sabido que ya ha sido recogido por el jugador
        }
            

        
        
            
        
    }
   
}
