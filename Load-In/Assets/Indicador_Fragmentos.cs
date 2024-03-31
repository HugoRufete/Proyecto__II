using UnityEngine;

public class RotarPivoteHaciaObjeto : MonoBehaviour
{
    public Transform objetoAsignado; // Objeto al que el pivote debe apuntar
    public RectTransform flecha; // Referencia al hijo (flecha) del pivote

    private bool yScaleModified = false;

    void LateUpdate()
    {
        if (objetoAsignado != null)
        {
            // Calcular la posici�n relativa del objeto asignado respecto a la c�mara
            Vector3 posicionRelativa = objetoAsignado.position - Camera.main.transform.position;

            // Calcular el �ngulo entre la posici�n relativa y el eje Y (arriba)
            float angulo = Mathf.Atan2(posicionRelativa.y, posicionRelativa.x) * Mathf.Rad2Deg;

            // Calcular el cuaterni�n de rotaci�n
            Quaternion targetRotation = Quaternion.AngleAxis(angulo, Vector3.forward);

            // Aplicar la rotaci�n al pivote
            transform.rotation = targetRotation;

            // Verificar si el �ngulo en Z es mayor que 90 o menor que -90 y ajustar la escala en Y
            if (!yScaleModified && (angulo > 90f || angulo < -90f))
            {
                Vector3 nuevaEscala = transform.localScale;
                nuevaEscala.y = -1;
                transform.localScale = nuevaEscala;
                yScaleModified = true;
            }
            else if (yScaleModified && (angulo <= 90f && angulo >= -90f))
            {
                Vector3 nuevaEscala = transform.localScale;
                nuevaEscala.y = 1;
                transform.localScale = nuevaEscala;
                yScaleModified = false;
            }

            // Ajustar la posici�n de la flecha para que est� dentro de la c�mara
            if (flecha != null)
            {
                Vector3 viewportPoint = Camera.main.WorldToViewportPoint(objetoAsignado.position);
                Vector3 posicionCorregida = new Vector3(
                    Mathf.Clamp(viewportPoint.x, 0.05f, 0.95f) * Screen.width,
                    Mathf.Clamp(viewportPoint.y, 0.05f, 0.95f) * Screen.height,
                    0f
                );
                flecha.position = posicionCorregida;
            }
        }
    }
}
