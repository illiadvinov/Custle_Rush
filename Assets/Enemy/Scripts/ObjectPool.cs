using System.Collections;
using UnityEngine;

namespace Assets.Enemy
{
    public class ObjectPool : MonoBehaviour
    {
        public InGameSettings settings;
        [SerializeField] private GameObject enemyPrefab;

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
            pool = new GameObject[settings.poolSize];

            for (int i = 0; i < pool.Length; i++)
            {
                pool[i] = Instantiate(enemyPrefab, transform);
                pool[i].SetActive(false);
            }


        }

        private void EnableObjectInPool()
        {
            for (int i = 0; i < pool.Length; i++)
            {
                if (pool[i].activeInHierarchy == false)
                {
                    pool[i].SetActive(true);
                    return;
                }
            }

        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                EnableObjectInPool();
                yield return new WaitForSeconds(settings.spawnTime);
            }

        }

    }
}
