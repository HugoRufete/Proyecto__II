using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public string textToWrite = "Me gustas las frambuesas, si no encuentro piso en Holanda me quedo en la puta calle"; 
    public float letterDelay = 0.1f; 
    private string currentText = "";
    private TextMeshProUGUI textComponent;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        // Comienza la animación para mostrar el texto
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= textToWrite.Length; i++)
        {
            currentText = textToWrite.Substring(0, i); 

            if (textComponent != null)
            {
                textComponent.text = currentText;
            }

            yield return new WaitForSeconds(letterDelay);
        }
    }
}
