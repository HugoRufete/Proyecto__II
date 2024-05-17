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

    public GameObject fragmento_1;
    public GameObject fragmento_2;
    public GameObject fragmento_3;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fragmento"))
        {
            fragmentosRecogidos++;

            other.gameObject.SetActive(false);

            if (fragmentosRecogidos >= 3)
            {
                fragmento_3.SetActive(true);
                areaExtraccion.SetActive(true);
                if (onFragmentosRecogidos != null)
                {
                    onFragmentosRecogidos.Invoke(fragmentosRecogidos);
                }
            }
            else if (fragmentosRecogidos == 1)
            {
                fragmento_1.SetActive(true);
            }
            else if (fragmentosRecogidos == 2)
            {
                fragmento_2.SetActive(true);
            }
        }
    }
}
