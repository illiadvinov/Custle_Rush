using System.Collections;
using UnityEngine;

namespace Assets.Enemy
{
    public class ObjectPool : MonoBehaviour
{
    public int PoolSize { set {poolSize = value;} }
    public float SpawnTime { set{spawnTime = value;} }
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] private int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] private float spawnTime = 1f;
    
    private GameObject[] pool;

    private void Awake() 
    {
        PopulatePool(); 
    }

    private void Start() 
    {
        StartCoroutine(SpawnEnemy());
        
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }

        
    }

    private void EnableObjectInPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }

    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTime);
        }

    }
   
}
}
