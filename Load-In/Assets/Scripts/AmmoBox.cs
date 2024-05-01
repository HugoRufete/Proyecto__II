using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public GameObject pistolPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Buscar el componente Pistol en los hijos del objeto pistolPrefab
            Pistol pistol = pistolPrefab.transform.GetComponentInChildren<Pistol>();

            // Verificar si se encontró el componente
            if (pistol != null)
            {
                Debug.Log("Recargando Pistola");
                pistol.RecargarPistola();
            }
            else
            {
                Debug.LogWarning("No se encontró el componente Pistol en la pistola instanciada.");
            }

            // Destruir el objeto vacío que contiene el collider 2D
            Destroy(gameObject);
        }
    }
}