using UnityEngine;

namespace Assets.Enemy
{
    [RequireComponent(typeof(Assets.Enemy.Enemy))]
    public class EnemyHealth : MonoBehaviour
    {
        public InGameSettings settings;

        private int currentHealth = 0;
        private Assets.Enemy.Enemy enemy;

        private void OnEnable()
        {
            currentHealth = settings.maxHitPoints;
        }

        private void Start()
        {
            settings.maxHitPoints = settings.defaultMaxHitPoints;
            enemy = FindObjectOfType<Assets.Enemy.Enemy>();
        }

        private void OnParticleCollision(GameObject other)
        {
            HitEnemy();
        }

        private void HitEnemy()
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                settings.maxHitPoints += settings.difficulty;
                enemy.RewardGold();

            }
        }

    }

}