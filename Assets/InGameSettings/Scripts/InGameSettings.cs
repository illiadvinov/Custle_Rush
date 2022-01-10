using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="InGame Settings")]
public class InGameSettings : ScriptableObject
{
    [Header ("\nBank.cs settings")]
    public int startBalance = 500;
    public int currentBalance = 0;

    [Header ("\nEnemy.cs settings")]
    public int goldReward = 25;
    public int goldPenalty = 30;

    [Header ("\nEnemyHealth.cs settings")]
    public int maxHitPoints = 7;
    public int difficulty = 3;

    [Header ("\nEnemyMover.cs settings")]
    [Range(0f, 10f)] public float speed = 1f;

    [Header ("\nObjectPool.cs settings")]
    [Range(0, 50)] public int poolSize = 5;
    [Range(0.1f, 30f)] public float spawnTime = 1f;

    [Header ("\nGridManager.cs settings")]
    public Vector2Int gridSize;

    [Header ("\nPathFinder.cs settings")]
    public Vector2Int startCoordinates = new Vector2Int(8,4);
    public Vector2Int endCoordinates = new Vector2Int(15, 12);

    [Header ("\nTargetLocator.cs settings")]
    public float towerRange = 15f;
    public float turnSpeed = 1f;

    [Header ("\nTower.cs settings")]
    public int cost = 75;
    public float buildDelay = 1f;
}
