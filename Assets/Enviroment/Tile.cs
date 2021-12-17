using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable {  get { return isPlaceable; } }

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder  = FindObjectOfType<PathFinder>();
    }

    void Start() 
    {
        if(gridManager)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position); // Getting position of current Tile (f.e. 10x;20y = 1,2)

            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates); // setting isWalkable to false
            }
        }
    }
 
    void OnMouseUp() 
    {
        
        if(gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
           bool isSuccesful = towerPrefab.CreateTower(towerPrefab, transform.position);
           if(isSuccesful) 
           {
               gridManager.BlockNode(coordinates);
               pathFinder.NotifyReceivers();
           }
        }
    }
}
