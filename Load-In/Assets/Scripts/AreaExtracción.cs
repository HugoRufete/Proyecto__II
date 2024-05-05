using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AreaExtracción : MonoBehaviour
{
    private bool playerInside = false;
    public float extractionTimer = 0f;
    private float extractionDuration = 15f;

    public GameObject timerExtraction;

    public Animator animator;

    // Update is called once per frame

    public TMP_Text extractionTimerText;
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
            extractionTimerText.text = "You will scape in 15 seconds! " + extractionTimer.ToString("F2") + " seconds";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timerExtraction.SetActive(true);
            animator.enabled = true;
            animator.Play("AreaExtracciónAnimation");
            playerInside = true;
            Debug.Log("El jugador ha entrado en el area de extracción");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timerExtraction.SetActive(false);
            animator.Play("AreaExtracciónIdleAnimation");
            playerInside = false;
            extractionTimer = 0f; 
        }
    }
}
