using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)]float speed = 0f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;
    
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
       
    }
    
    void Awake() 
    {
        enemy = FindObjectOfType<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coord = new Vector2Int();
        if(resetPath) coord = pathFinder.StartCoordinates;
        else coord = gridManager.GetCoordinatesFromPosition(transform.position);

        StopAllCoroutines();

        path.Clear();
        path = pathFinder.GetNewPath(coord);

        StartCoroutine(FollowPath());
       
    }

   

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
        //if(transform.position != null) Debug.Log(transform.position);

    }

    void FinishPath()
    {
         enemy.StealGold();
         gameObject.SetActive(false);

    }

    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            
            transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }

        FinishPath();  
    }
    

}
