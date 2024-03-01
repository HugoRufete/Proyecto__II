using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<EnemySpawnInfo> enemies;  // Lista que contendrá información sobre los enemigos en la oleada
}

[System.Serializable]
public class EnemySpawnInfo
{
    public EnemyType enemyType;  // Tipo de enemigo
    public int count;  // Cantidad de enemigos de este tipo en la oleada
    public float timeBetweenSpawns;  // Tiempo entre cada instancia de este tipo de enemigo
}

public enum EnemyType
{
    Basic,
    Firefly
}
