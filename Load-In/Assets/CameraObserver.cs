using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObserver : MonoBehaviour
{
    public List<SpawnerIdentifier> spawnerIdentifiers;

    private List<GameObject> objectsToObserve;

    void Start()
    {
        objectsToObserve = new List<GameObject>();

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.activeInHierarchy)
            {
                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null && !collider.isTrigger)
                {
                    objectsToObserve.Add(obj);
                }
                else
                {
                    Transform parent = obj.transform.parent;
                    while (parent != null)
                    {
                        collider = parent.GetComponent<Collider2D>();
                        if (collider != null && !collider.isTrigger)
                        {
                            objectsToObserve.Add(obj);
                            break;
                        }
                        parent = parent.parent;
                    }
                }
            }
        }

        objectsToObserve = objectsToObserve.FindAll(obj => obj.GetComponent<SpawnerIdentifier>() != null);
    }

    void Update()
    {
        List<GameObject> objectsToActivate = new List<GameObject>();

        foreach (GameObject obj in objectsToObserve)
        {
            Collider2D collider = obj.GetComponent<Collider2D>();
            if (collider == null || collider.isTrigger) continue;

            Vector3 screenPoint = Camera.main.WorldToViewportPoint(collider.bounds.center);
            if (screenPoint.x > 0 && screenPoint.x < 1 &&
                screenPoint.y > 0 && screenPoint.y < 1 &&
                screenPoint.z > 0)
            {
                // Si el objeto est� dentro del campo de visi�n de la c�mara, se desactiva
                obj.SetActive(false);
            }
            else
            {
                // Si el objeto no est� dentro del campo de visi�n de la c�mara, se agrega a la lista de objetos a activar
                objectsToActivate.Add(obj);
            }
        }

        foreach (GameObject obj in objectsToActivate)
        {
            // Si el objeto no est� dentro del campo de visi�n de la c�mara, se activa
            obj.SetActive(true);
        }
    }
}