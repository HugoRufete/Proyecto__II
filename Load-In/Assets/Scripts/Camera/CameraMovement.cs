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
