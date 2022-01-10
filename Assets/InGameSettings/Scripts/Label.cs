using UnityEngine;

public class Label : MonoBehaviour
{
    [Header ("\nBank.cs settings")]
    [SerializeField] private int startBalance = 500;
    [SerializeField] private int currentBalance = 0;

    [Header ("\nEnemy.cs settings")]
    [SerializeField] private int goldReward = 25;
    [SerializeField] private int goldPenalty = 30;

    [Header ("\nEnemyHealth.cs settings")]
    [SerializeField] private int maxHitPoints = 7;
    [SerializeField] private int difficulty = 3;

    [Header ("\nEnemyMover.cs settings")]
    [SerializeField] [Range(0f, 10f)] private float speed = 1f;

    [Header ("\nObjectPool.cs settings")]
    [SerializeField] [Range(0, 50)] private int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] private float spawnTime = 1f;

    [Header ("\nGridManager.cs settings")]
    [SerializeField] private Vector2Int gridSize;

    [Header ("\nPathFinder.cs settings")]
    [SerializeField] private Vector2Int startCoordinates = new Vector2Int(8,4);
    [SerializeField] private Vector2Int endCoordinates = new Vector2Int(15, 12);

    [Header ("\nTargetLocator.cs settings")]
    [SerializeField] private float towerRange = 15f;
    [SerializeField] private float turnSpeed = 1f;

    [Header ("\nTower.cs settings")]
     [SerializeField] private int cost = 75;
    [SerializeField] private float buildDelay = 1f;


    private void Awake()

    {
        FindObjectOfType<Assets.Bank.Bank>().StartBalance = startBalance;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.A)) FindObjectOfType<Assets.Bank.Bank>().Deposit(currentBalance);

        Enemy();

        EnemyHealth();

        EnemyMover();

        ObjectPool();

        GridManager();

        PathFinder();

        TargetLocator();
        
        Tower();
    }


    private void Tower()
    {
        if (FindObjectOfType<Assets.Tower.Tower>() != null)
        {
            FindObjectOfType<Assets.Tower.Tower>().Cost = cost;
            FindObjectOfType<Assets.Tower.Tower>().BuildDelay = buildDelay;
        }
    }

    private void TargetLocator()
    {
        if (FindObjectOfType<Assets.Tower.TargetLocator>() != null)
        {
            FindObjectOfType<Assets.Tower.TargetLocator>().TowerRange = towerRange;
            FindObjectOfType<Assets.Tower.TargetLocator>().TurnSpeed = turnSpeed;
        }
    }

    private void PathFinder()
    {
        FindObjectOfType<Assets.PathFinding.PathFinder>().StartCoordinates = startCoordinates;
        FindObjectOfType<Assets.PathFinding.PathFinder>().EndCoordinates = endCoordinates;
    }

    private void GridManager()
    {
        FindObjectOfType<Assets.PathFinding.GridManager>().GridSize = gridSize;
    }

    private void ObjectPool()
    {
        FindObjectOfType<Assets.Enemy.ObjectPool>().PoolSize = poolSize;
        FindObjectOfType<Assets.Enemy.ObjectPool>().SpawnTime = spawnTime;
    }

    private void EnemyMover()
    {
        if (FindObjectOfType<Assets.Enemy.EnemyMover>() != null)
        {
            FindObjectOfType<Assets.Enemy.EnemyMover>().Speed = speed;
        }
    }

    private void EnemyHealth()
    {
        if (FindObjectOfType<Assets.Enemy.EnemyHealth>() != null)
        {
            FindObjectOfType<Assets.Enemy.EnemyHealth>().MaxHitPoints = maxHitPoints;
            FindObjectOfType<Assets.Enemy.EnemyHealth>().Difficulty = difficulty;
        }
    }

    private void Enemy()
    {
        if (FindObjectOfType<Assets.Enemy.Enemy>() != null)
        {
            FindObjectOfType<Assets.Enemy.Enemy>().GoldPenalty = goldPenalty;
            FindObjectOfType<Assets.Enemy.Enemy>().GoldReward = goldReward;
        }
    }
}
