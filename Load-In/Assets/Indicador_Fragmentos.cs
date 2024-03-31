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
            // Calcular la posición relativa del objeto asignado respecto a la cámara
            Vector3 posicionRelativa = objetoAsignado.position - Camera.main.transform.position;

            // Calcular el ángulo entre la posición relativa y el eje Y (arriba)
            float angulo = Mathf.Atan2(posicionRelativa.y, posicionRelativa.x) * Mathf.Rad2Deg;

            // Calcular el cuaternión de rotación
            Quaternion targetRotation = Quaternion.AngleAxis(angulo, Vector3.forward);

            // Aplicar la rotación al pivote
            transform.rotation = targetRotation;

            // Verificar si el ángulo en Z es mayor que 90 o menor que -90 y ajustar la escala en Y
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

            // Ajustar la posición de la flecha para que esté dentro de la cámara
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
