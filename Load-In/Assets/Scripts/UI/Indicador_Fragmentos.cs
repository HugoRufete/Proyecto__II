using UnityEngine;

public class RotarPivoteHaciaObjeto : MonoBehaviour
{
    public Transform objetoAsignado; public Transform flecha; public Transform jugador; public RectTransform minimapRect;

    private bool yScaleModified = false;

    public float radioDestruccion = 7.0f;

    private Vector3 originalFlechaPosition;

    void Start()
    {  originalFlechaPosition = flecha.localPosition; }

    void Update()
    {
        if (objetoAsignado != null && jugador != null)
        {
            Vector3 posicionRelativa = objetoAsignado.position - Camera.main.transform.position;


            float angulo = Mathf.Atan2(posicionRelativa.y, posicionRelativa.x) * Mathf.Rad2Deg;

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

            // Calculate the position of the flecha within the minimap
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(objetoAsignado.position);
            Vector3 minimapPoint = new Vector3(
                Mathf.Clamp01(viewportPoint.x) * minimapRect.rect.width,
                Mathf.Clamp01(viewportPoint.y) * minimapRect.rect.height,
                0f
            );

            // Set the arrow position relative to its original position
            flecha.localPosition = originalFlechaPosition;

            float distanciaJugadorObjeto = Vector3.Distance(jugador.position, objetoAsignado.position);
            if (distanciaJugadorObjeto <= radioDestruccion)
            {
                // Destruir la flecha
                if (flecha != null)
                {
                    Destroy(flecha.gameObject);
                }
            }
        }
        else
        {
            if (flecha != null)
            {
                Destroy(flecha.gameObject);
            }
        }
    }


}