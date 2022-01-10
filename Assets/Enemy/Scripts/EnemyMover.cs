using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Enemy
{
    [RequireComponent(typeof(Assets.Enemy.Enemy))]
public class EnemyMover : MonoBehaviour
{
    public InGameSettings settings;

    private List<Assets.PathFinding.Node> path = new List<Assets.PathFinding.Node>();

    private Assets.Enemy.Enemy enemy;
    private Assets.PathFinding.GridManager gridManager;
    private Assets.PathFinding.PathFinder pathFinder;
    
    private void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
       
    }
    
    private void Awake() 
    {
        enemy = FindObjectOfType<Assets.Enemy.Enemy>();
        gridManager = FindObjectOfType<Assets.PathFinding.GridManager>();
        pathFinder = FindObjectOfType<Assets.PathFinding.PathFinder>();
    
    }

    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coord = new Vector2Int();
        if(resetPath) coord = pathFinder.settings.startCoordinates;
        else coord = gridManager.GetCoordinatesFromPosition(transform.position);

        StopAllCoroutines();

        path.Clear();
        path = pathFinder.GetNewPath(coord);

        StartCoroutine(FollowPath());
       
    }

   
    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.settings.startCoordinates);
    }

    private void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    private IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            
            transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * settings.speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }

        FinishPath();  
    }
    

}

}
