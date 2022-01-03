using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Tower
{
    public class TargetLocator : MonoBehaviour
{
    public float TowerRange {  set{towerRange = value;} }
    public float TurnSpeed {  set{turnSpeed = value;} }
    [SerializeField] private Transform weapon;
    [SerializeField] private ParticleSystem projectileParticles;
    [SerializeField] private float towerRange = 15f;
    [SerializeField] private float turnSpeed = 1f;
    private Quaternion rotGoal;
    private Vector3 direction;

    private Transform target;
    
    private void Update() 
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        
       Assets.Enemy.Enemy[] enemies = FindObjectsOfType<Assets.Enemy.Enemy>();
       Transform closestTarget = null;
       float maxDistance = Mathf.Infinity;

       foreach(Assets.Enemy.Enemy enemy in enemies)
       {
           float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

           if(targetDistance < maxDistance)
           {
               closestTarget = enemy.transform;
               maxDistance = targetDistance;
           }
       }
       target = closestTarget;
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        direction = (transform.position - target.position);
        rotGoal = Quaternion.LookRotation(-direction);
        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, rotGoal, Time.deltaTime * turnSpeed);

        if(targetDistance < towerRange) Attack(true);
        else Attack(false);
    }
    
    private void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;

        emissionModule.enabled = isActive;
    }
}
}
