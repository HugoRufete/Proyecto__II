using UnityEngine;
using System.Collections.Generic;

public class AmmoBox : MonoBehaviour
{
    private List<IRecargable> _recargables;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindAndAssignRecargables(collision.gameObject.transform);

            foreach (var recargable in _recargables)
            {
                recargable.RecargarArma();
            }

            Destroy(gameObject);
        }
    }

    private void FindAndAssignRecargables(Transform root)
    {
        List<GameObject> gameObjects = new List<GameObject>(FindObjectsOfType<GameObject>(true));

        _recargables = new List<IRecargable>();

        foreach (var go in gameObjects)
        {
            IRecargable recargable = go.GetComponent<IRecargable>();

            if (recargable != null)
            {
                _recargables.Add(recargable);
            }
        }

        if (_recargables.Count == 0 && root == null)
        {
            Debug.LogError("No se encontraron armas recargables en la escena.");
        }
    }
}