using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumMun : MonoBehaviour
{
    public int NumCajas;
    public TMP_Text text;

    public GameObject Cajas;
    void Start()
    {
        NumCajas = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        if (NumCajas == 0)
        {
            Cajas.SetActive(false);
        }

        else if (NumCajas > 0)
        {
            Cajas.SetActive(true);
            text.text = NumCajas.ToString();
        }
    }
}
