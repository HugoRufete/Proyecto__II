using UnityEngine;

public class Equipar : MonoBehaviour
{
    public GameObject basic_weapon;  // Asigna el prefab del arma desde el Inspector
    public GameObject sword;  // Asigna el prefab del arma desde el Inspector
    public GameObject shotgun;  // Asigna el prefab del arma desde el Inspector
    public Transform puntoDeEquipamiento;  // Punto donde se instanciarán las armas

    void Update()
    {
        // Ejemplo: Presionar la tecla "E" para equipar el arma
        if (Input.GetKeyDown(KeyCode.E))
        {
            EquiparShotGun();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            EquiparSword();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquiparBasicWeapon();
        }

    }

    void EquiparShotGun()
    {
        if (shotgun != null)
        {
            // Encuentra el objeto del jugador (asume que solo hay un objeto de jugador en la escena)
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
                // Instancia el prefab del arma y lo hace hijo del jugador
                Instantiate(shotgun, puntoDeEquipamiento.position, Quaternion.identity, jugador.transform);
                Debug.Log("Arma equipada");
                
            }
        }
    }

    void EquiparSword()
    {
        if (sword != null)
        {
        // Encuentra el objeto del jugador (asume que solo hay un objeto de jugador en la escena)
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
            // Instancia el prefab del arma con una rotación de -45 grados en Z y lo hace hijo del jugador
            Instantiate(sword, puntoDeEquipamiento.position, Quaternion.Euler(0f, 0f, -45f), jugador.transform);
            Debug.Log("Arma equipada");
            }
        }   
    
    }

    void EquiparBasicWeapon()
    {
        if (basic_weapon != null)
        {
            // Encuentra el objeto del jugador (asume que solo hay un objeto de jugador en la escena)
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
                // Instancia el prefab del arma con una rotación de -45 grados en Z y lo hace hijo del jugador
                Instantiate(basic_weapon, puntoDeEquipamiento.position, Quaternion.Euler(0f, 0f, -45f), jugador.transform);
                Debug.Log("Arma equipada");
            }
        }

    }


}
