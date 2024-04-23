using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEsporas : MonoBehaviour
{
    [SerializeField] private int esporas;
    public TMP_Text textoEsporas;

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
        esporas = esporas + 50;
    }

    public int GetEsporas()
    {
        return esporas;
    }

    private void ActualizarTextoEsporas()
    {
        if (textoEsporas != null)
        {
            textoEsporas.text = esporas.ToString();
        }
    }
}
