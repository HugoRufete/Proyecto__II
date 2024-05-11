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

    private Coroutine zoomCoroutine;

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

        // Si hay una animación de zoom en progreso, la detenemos
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
    }

    public void Zoom(float intensidad, float tiempo)
    {
        float targetOrthographicSize = originalOrthographicSize * intensidad;
        zoomCoroutine = StartCoroutine(DoZoom(targetOrthographicSize, tiempo));
    }

    private IEnumerator DoZoom(float targetOrthographicSize, float tiempo)
    {
        float t = 0f;

        while (t < tiempo)
        {
            float newSize = Mathf.Lerp(cinemachinevirtualcamera.m_Lens.OrthographicSize, targetOrthographicSize, t / tiempo);
            cinemachinevirtualcamera.m_Lens.OrthographicSize = newSize;

            // También interpolamos la posición de la cámara
            transform.position = Vector3.Lerp(transform.position, originalPosition, t / tiempo);

            t += Time.deltaTime;
            yield return null;
        }

        cinemachinevirtualcamera.m_Lens.OrthographicSize = targetOrthographicSize;
        transform.position = originalPosition;
    }

    public void RestaurarCamaraOriginal()
    {
        StartCoroutine(RestaurarCamara());
    }

    private IEnumerator RestaurarCamara()
    {
        float t = 0f;

        while (t < tiempoMovimientoTotal)
        {
            float newSize = Mathf.Lerp(cinemachinevirtualcamera.m_Lens.OrthographicSize, originalOrthographicSize, t / tiempoMovimientoTotal);
            cinemachinevirtualcamera.m_Lens.OrthographicSize = newSize;

            t += Time.deltaTime;

            // También interpolamos la posición de la cámara
            transform.position = Vector3.Lerp(transform.position, originalPosition, t / tiempoMovimientoTotal);

            yield return null;
        }

        cinemachinevirtualcamera.m_Lens.OrthographicSize = originalOrthographicSize;
        transform.position = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempoMovimiento > 0)
        {
            tiempoMovimiento -= Time.deltaTime;
            cinemachinebasicmultichanelperlin.m_AmplitudeGain = Mathf.Lerp(intensidadInicial, 0, 1 - (tiempoMovimiento / tiempoMovimientoTotal));
        }
    }
}
