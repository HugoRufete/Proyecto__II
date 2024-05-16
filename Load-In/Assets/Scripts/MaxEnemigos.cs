using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxEnemigos : MonoBehaviour
{
    public int numEnemies;
    public int maxEnemies;
    public bool MaxSuperated;

    public void Update()
    {
        if (numEnemies < maxEnemies)
        {
            MaxSuperated = false;
        }
        if (numEnemies >= maxEnemies)
        {
            Debug.Log("AAAA");
            MaxSuperated = true;
        }
    }
}
