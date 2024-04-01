using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExtracción : MonoBehaviour
{
    private bool playerInside = false;
    public float extractionTimer = 0f;
    private float extractionDuration = 5f;

    // Update is called once per frame
    void Update()
    {
        if (playerInside)
        {
            extractionTimer += Time.deltaTime;

            if (extractionTimer >= extractionDuration)
            {
                SceneManager.LoadScene("WinScene");
                Debug.Log("¡Extracción completada!");
                extractionTimer = 0f; // Reiniciar el temporizador
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("El jugador ha entrado en el area de extracción");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            extractionTimer = 0f; 
        }
    }
}
