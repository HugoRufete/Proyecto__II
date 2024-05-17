using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Créditos : MonoBehaviour
{
    public GameObject créditosObject;
    public GameObject lastText;
    void Start()
    {
        StartCoroutine(ActivateObjectWithDelay(créditosObject, 20f));
        StartCoroutine(ActivateSceneWithDelay("MainMenu", 35f));
        StartCoroutine(DestroyObjectCoroutine(lastText, 20f));
    }

    private void ActivarCréditos()
    {
        créditosObject.SetActive(true);
        
    }

    private IEnumerator ActivateSceneWithDelay(string nombreEscena, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (nombreEscena != null)
        {
            SceneManager.LoadScene(nombreEscena);
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
    private IEnumerator ActivateObjectWithDelay(GameObject objetoAActivar, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objetoAActivar != null)
        {
            objetoAActivar.SetActive(true);
        }
    }
}
