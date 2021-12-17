using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 7;
    [Tooltip("Adds amount to maxHitPoints when enemy dies")]
    [SerializeField] int difficultyRamp = 3;
    int currentHealth = 0;

    Enemy enemy;

    void OnEnable() 
    {
        
        currentHealth = maxHitPoints; 
    }

    void Start() 
    {
        enemy = FindObjectOfType<Enemy>();
    }

    private void OnParticleCollision(GameObject other) 
    {
        CrashEnemy(); 
    }

    void CrashEnemy()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
            
        }
    }
   
}
