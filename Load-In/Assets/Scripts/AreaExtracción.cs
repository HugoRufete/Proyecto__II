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

    public AudioSource audioSource;
    public AudioClip audioClip1, audioClip2;

    public GameObject timerExtraction;

    public Animator animator;

    public Animator animatorExplosión;

    public GameObject explosionObject;

    FreezeEnemies[] freezeEnemies;
    // Update is called once per frame

    public TMP_Text extractionTimerText;

    public GameObject panelEndGame;

    public GameObject UI;

    private void Start()
    {
        freezeEnemies = FindObjectsOfType<FreezeEnemies>();
    }
    void Update()
    {
        
        if (playerInside)
        {
            extractionTimer += Time.deltaTime;

            if (extractionTimer >= extractionDuration)
            {
                audioSource.clip = audioClip1;
                audioSource.Play();
                StartCoroutine(ActivateObjectWithDelay(panelEndGame, 4f));
                StartCoroutine(ActivateSceneWithDelay("CreditosEscena", 7f));


                UI.SetActive(false);
                explosionObject.SetActive(true);
                for (int i = 0; i < freezeEnemies.Length; i++)
                {
                    freezeEnemies[i].DesactivarComponentes();
                }
                Debug.Log("¡Extracción completada!");
                extractionTimer = 0f; 
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
            animator.Play("AreaExtracciónIdleAnimation");
            playerInside = false;
            extractionTimer = 0f; 
        }
    }
    private IEnumerator ActivateSoundWithDelay(AudioClip audioClip, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (audioSource != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
    private IEnumerator ActivateObjectWithDelay(GameObject objetoAActivar, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoAActivar != null)
        {
            objetoAActivar.SetActive(true);
        }
    }

    private IEnumerator ActivateSceneWithDelay(string nombreEscena, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (nombreEscena != null)
        {
            SceneManager.LoadScene(nombreEscena);
        }

        
    }
}
