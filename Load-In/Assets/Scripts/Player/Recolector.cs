using System;
using UnityEngine;
using UnityEngine.Events;

public class Recolector : MonoBehaviour
{
    public int fragmentosRecogidos = 0;

    public GameObject areaExtraccion;

    [Serializable]
    public class FragmentosRecogidosEvent : UnityEvent<int> { }
    public FragmentosRecogidosEvent onFragmentosRecogidos;

    public GameObject indicardorArea;

    public GameObject indicadorFragmentos_1;
    public GameObject indicadorFragmentos_2;
    public GameObject indicadorFragmentos_3;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fragmento"))
        {
            fragmentosRecogidos++;

            other.gameObject.SetActive(false);

            if (fragmentosRecogidos >= 3)
            {
                areaExtraccion.SetActive(true);
                if (onFragmentosRecogidos != null)
                {
                    onFragmentosRecogidos.Invoke(fragmentosRecogidos);
                    indicardorArea.SetActive(true);

                    indicadorFragmentos_1.SetActive(false);
                    indicadorFragmentos_2.SetActive(false);
                    indicadorFragmentos_3.SetActive(false);


                }
            }
        }
    }
}
