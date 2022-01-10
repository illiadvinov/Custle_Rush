using UnityEngine;

namespace Assets.Enemy
{
    [RequireComponent(typeof(Assets.Enemy.Enemy))]
    public class EnemyHealth : MonoBehaviour
{
    public int MaxHitPoints { set {maxHitPoints = value;} }
    public int Difficulty { set {difficulty = value;} }
    [SerializeField] private int maxHitPoints = 7;
    [SerializeField] private int difficulty = 3;
    private int currentHealth = 0;

    private Assets.Enemy.Enemy enemy;

    private void OnEnable() 
    {
        currentHealth = maxHitPoints; 
    }

    private void Start() 
    {
        enemy = FindObjectOfType<Assets.Enemy.Enemy>();
    }

    private void OnParticleCollision(GameObject other) 
    {
        HitEnemy(); 
    }

    private void HitEnemy()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficulty;
            enemy.RewardGold();
            
        }
    }
   
}

}