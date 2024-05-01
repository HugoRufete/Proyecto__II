using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaEsporas : MonoBehaviour
{
    public GameObject prefabToInstantiate;

    public float instantiationRadius = 0.5f;

    public IEnumerator InstantiateEsporas(float cantidadEsporas)
    {
        yield return null;

        
        Vector3 centerPosition = transform.position;

        
        List<GameObject> instantiatedPrefabs = new List<GameObject>();

        
        for (int i = 0; i < cantidadEsporas; i++)
        {
            Vector3 randomPosition = centerPosition + new Vector3(
                Random.Range(-instantiationRadius, instantiationRadius),
                Random.Range(-instantiationRadius, instantiationRadius),
                Random.Range(-instantiationRadius, instantiationRadius)
            );
            GameObject prefab = Instantiate(prefabToInstantiate, randomPosition, Quaternion.identity, transform);

            instantiatedPrefabs.Add(prefab);
        }
       
    }
}