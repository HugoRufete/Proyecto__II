using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public string textToWrite = "Me gustas las frambuesas, si no encuentro piso en Holanda me quedo en la puta calle"; 
    public float letterDelay = 0.1f; // Retraso entre cada letra (en segundos)
    private string currentText = "";
    private TextMeshProUGUI textComponent;

    void Awake()
    {
        // Obtén una referencia al componente TextMeshProUGUI
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
            currentText = textToWrite.Substring(0, i); // Obtén el texto actual hasta la letra i

            if (textComponent != null)
            {
                textComponent.text = currentText; // Actualiza el componente de texto con el texto actual
            }

            // Espera un breve tiempo antes de mostrar la siguiente letra
            yield return new WaitForSeconds(letterDelay);
        }
    }
}
