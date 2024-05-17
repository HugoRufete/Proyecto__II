using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip audioClip1, audioClip2;

    public GameObject textoInicial;

    public SimplePlayerMovement playerController;

    [Header("Objetos a activar después del texto")]

    public GameObject UI1;
    public GameObject UI2;
    public GameObject UI3;
    public GameObject UI4;
    public GameObject UI5;
    void Start()
    {
        playerController.enabled = false;

        audioSource.clip = audioClip1;
        audioSource.Play();
        StartCoroutine(ActivateSoundWithDelay(audioClip2, 10f));

        StartCoroutine(ActivateObjectWithDelay(textoInicial, 10f));
        StartCoroutine(EnableObjectWithDelay(playerController, 70f));

        StartCoroutine(DestroyObjectCoroutine(textoInicial, 70f));
        StartCoroutine(ActivateObjectWithDelay(UI1, 70f));
        StartCoroutine(ActivateObjectWithDelay(UI2, 70f));
        StartCoroutine(ActivateObjectWithDelay(UI3, 70f));
        StartCoroutine(ActivateObjectWithDelay(UI4, 70f));
        StartCoroutine(ActivateObjectWithDelay(UI5, 70f));
        

    }
    private IEnumerator ActivateObjectWithDelay(GameObject objetoAActivar, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoAActivar != null)
        {
            objetoAActivar.SetActive(true);
        }
    }

    private IEnumerator DestroyObjectCoroutine(GameObject objetoADestruir, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoADestruir != null)
        {
            objetoADestruir.SetActive(false);
        }
    }
    private IEnumerator EnableObjectWithDelay(SimplePlayerMovement objetoADestruir, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoADestruir != null)
        {
            objetoADestruir.enabled = true;
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
}
