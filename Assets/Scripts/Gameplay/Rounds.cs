using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Round
{
    public List<EnemyTypeCount> enemies;
}

[System.Serializable]
public class EnemyTypeCount
{
    public GameObject enemyPrefab;
    public int count;
}