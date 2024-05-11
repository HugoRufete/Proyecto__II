using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;

    private CinemachineVirtualCamera cinemachinevirtualcamera;

    private CinemachineBasicMultiChannelPerlin cinemachinebasicmultichanelperlin;

    private float tiempoMovimiento;

    private float tiempoMovimientoTotal;

    private float intensidadInicial;

    private float originalOrthographicSize;

    private Vector3 originalPosition; 

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        cinemachinevirtualcamera = GetComponent<CinemachineVirtualCamera>();
        cinemachinebasicmultichanelperlin = cinemachinevirtualcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void MoverCamara(float intensidad, float frecuencia, float tiempo)
    {
        cinemachinebasicmultichanelperlin.m_AmplitudeGain = intensidad;
        cinemachinebasicmultichanelperlin.m_FrequencyGain = frecuencia;
        intensidadInicial = intensidad;
        tiempoMovimientoTotal = tiempo;
        tiempoMovimiento = tiempo;

        originalOrthographicSize = cinemachinevirtualcamera.m_Lens.OrthographicSize;
        originalPosition = transform.position;

    }

    public void Zoom(float intensidad, float tiempo)
    {
        float targetOrthographicSize = originalOrthographicSize * intensidad;
        StartCoroutine(DoZoom(targetOrthographicSize, tiempo));
    }

    private IEnumerator DoZoom(float targetOrthographicSize, float tiempo)
    {
        float t = 0f;

        while (t < tiempo)
        {
            // Interpolamos suavemente entre el tamaño actual y el tamaño objetivo
            float newSize = Mathf.Lerp(cinemachinevirtualcamera.m_Lens.OrthographicSize, targetOrthographicSize, t / tiempo);
            cinemachinevirtualcamera.m_Lens.OrthographicSize = newSize;

            t += Time.deltaTime;
            yield return null;
        }

        // Asegúrate de establecer el tamaño de la cámara al tamaño objetivo después del zoom
        cinemachinevirtualcamera.m_Lens.OrthographicSize = targetOrthographicSize;
    }

    public void RestaurarCamaraOriginal()
    {
        StartCoroutine(RestaurarCamara());
    }

    private IEnumerator RestaurarCamara()
    {
        float t = 0f;

        // Restaurar la posición de la cámara de forma inmediata
        transform.position = originalPosition;

        while (t < tiempoMovimientoTotal)
        {
            // Interpolamos suavemente entre el tamaño actual y el tamaño original
            float newSize = Mathf.Lerp(cinemachinevirtualcamera.m_Lens.OrthographicSize, originalOrthographicSize, t / tiempoMovimientoTotal);
            cinemachinevirtualcamera.m_Lens.OrthographicSize = newSize;

            t += Time.deltaTime;
            yield return null;
        }

        // Asegúrate de que el tamaño de la cámara esté en su valor original al finalizar la transición
        cinemachinevirtualcamera.m_Lens.OrthographicSize = originalOrthographicSize;
    }
    // Update is called once per frame
    void Update()
    {
        if (tiempoMovimiento > 0)
        {
            tiempoMovimiento -= Time.deltaTime;
            cinemachinebasicmultichanelperlin.m_AmplitudeGain = Mathf.Lerp(intensidadInicial,0,1-(tiempoMovimiento/tiempoMovimientoTotal));
        }
    }
}
