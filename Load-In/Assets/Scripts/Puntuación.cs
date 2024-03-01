using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//Para poder hacer referencias al textmeshpro

public class Puntuación : MonoBehaviour
{
    private float puntos;
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        puntos += Time.deltaTime;
        textMesh.text = puntos.ToString("0");
    }

    public void sumarPuntos(float puntosEntrada)
    {
        puntos += puntosEntrada;
 
    }


}
