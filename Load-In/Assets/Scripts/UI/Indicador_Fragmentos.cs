using UnityEngine;

public class RotarPivoteHaciaObjeto : MonoBehaviour
{
    public Transform objetoAsignado; 
    public RectTransform flecha; 
    public Transform jugador; 

    private bool yScaleModified = false;

    public float radioDestruccion = 7.0f; 

    void LateUpdate()
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
