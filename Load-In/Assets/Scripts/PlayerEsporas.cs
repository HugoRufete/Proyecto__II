using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEsporas : MonoBehaviour
{
    [SerializeField] private float esporas;
    public TMP_Text textoEsporas;

    public int cantidadEsporasporPorPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espora"))
        {
            RecogerEsporas();
            Destroy(collision.gameObject);
            ActualizarTextoEsporas();
        }
    }

    public void RecogerEsporas()
    {
        esporas += cantidadEsporasporPorPrefab;
    }

    public float GetEsporas()
    {
        return esporas; 
    }

    public void RestarEsporas(float cantidad)
    {
        if (cantidad > 0 && cantidad <= esporas)
        {
            esporas -= cantidad;
            ActualizarTextoEsporas();
        }
        else
        {
            Debug.LogWarning("No tienes suficientes esporas para restar esta cantidad.");
        }
    }

    private void ActualizarTextoEsporas()
    {
        if (textoEsporas != null)
        {
            textoEsporas.text = esporas.ToString();
        }
    }
}
