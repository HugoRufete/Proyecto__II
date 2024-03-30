using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public GameObject pistolPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener el componente Pistol del prefab de la pistola
            Pistol pistol = pistolPrefab.GetComponentInChildren<Pistol>();

            // Verificar si se encontró el componente
            if (pistol != null)
            {
                // Llamar al método RecargarPistola
                pistol.RecargarPistola();
            }
            else
            {
                Debug.LogWarning("No se encontró el componente Pistol en el prefab de la pistola.");
            }
            Destroy(gameObject);
        }
    }
}
