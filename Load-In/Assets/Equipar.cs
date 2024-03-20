using UnityEngine;

public class Equipar : MonoBehaviour
{
    public GameObject basic_weapon;  
    public GameObject sword; 
    public GameObject shotgun;  
    public Transform puntoDeEquipamiento;
    public GameObject desactivarHUD;

   

    public void EquiparShotGun()
    {
        if (shotgun != null)
        {
            
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
                
                Instantiate(shotgun, puntoDeEquipamiento.position, Quaternion.identity, jugador.transform);
                desactivarHUD.SetActive(false);
                Time.timeScale = 1.0f;
                Debug.Log("Arma equipada");
                
            }
        }
    }

    public void EquiparSword()
    {
        if (sword != null)
        {
        // Encuentra el objeto del jugador (asume que solo hay un objeto de jugador en la escena)
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
            
            Instantiate(sword, puntoDeEquipamiento.position, Quaternion.Euler(0f, 0f, -45f), jugador.transform);
            Debug.Log("Arma equipada");
            }
        }   
    
    }

    public void EquiparBasicWeapon()
    {
        if (basic_weapon != null)
        {
           
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
                
                Instantiate(basic_weapon, puntoDeEquipamiento.position, Quaternion.Euler(0f, 0f, -45f), jugador.transform);
                Debug.Log("Arma equipada");
            }
        }

    }


}
