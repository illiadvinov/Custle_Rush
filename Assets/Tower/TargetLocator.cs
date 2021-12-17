using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float towerRange = 15f;
    [SerializeField] float turnSpeed = 1f;
    Quaternion rotGoal;
    Vector3 direction;

    Transform target;
    
    void Update() 
    {
        FindClosestTarget();
        AimWeapon();
        
    }

    void FindClosestTarget()
    {
        
       Enemy[] enemies = FindObjectsOfType<Enemy>();
       Transform closestTarget = null;
       float maxDistance = Mathf.Infinity;

       foreach(Enemy enemy in enemies)
       {
           float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
           //Debug.Log(targetDistance);

           if(targetDistance < maxDistance)
           {
               closestTarget = enemy.transform;
            
               maxDistance = targetDistance;
               
           }
       }

       target = closestTarget;


    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        direction = (transform.position - target.position);
        rotGoal = Quaternion.LookRotation(-direction);
        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, rotGoal, Time.deltaTime * turnSpeed);
        
        //weapon.LookAt(target);

        if(targetDistance < towerRange) Attack(true);
        else Attack(false);
        
    }
    
    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;

        emissionModule.enabled = isActive;
        
    }
}
