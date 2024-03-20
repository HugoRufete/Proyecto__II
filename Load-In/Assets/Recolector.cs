using System;
using UnityEngine;
using UnityEngine.Events;

public class Recolector : MonoBehaviour
{
    public int fragmentosRecogidos = 0; 

    [Serializable]
    public class FragmentosRecogidosEvent : UnityEvent<int> { }
    public FragmentosRecogidosEvent onFragmentosRecogidos;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fragmento"))
        {
            fragmentosRecogidos++;

            other.gameObject.SetActive(false);

            if (fragmentosRecogidos >= 3)
            {
                if (onFragmentosRecogidos != null)
                {
                    onFragmentosRecogidos.Invoke(fragmentosRecogidos);
                }
            }
        }
    }
}
