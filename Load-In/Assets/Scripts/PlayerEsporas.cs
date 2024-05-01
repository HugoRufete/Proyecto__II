using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEsporas : MonoBehaviour
{
    [SerializeField] private int esporas;
    public TMP_Text textoEsporas;

    public int cantidadEsporasporPrefab;

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
        esporas += cantidadEsporasporPrefab;
    }

    public int GetEsporas()
    {
        return esporas; 
    }

    public void RestarEsporas(int cantidad)
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
