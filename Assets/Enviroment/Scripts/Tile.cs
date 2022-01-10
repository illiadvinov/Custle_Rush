using UnityEngine;

namespace Assets.Environment
{
    public class Tile : MonoBehaviour
{

    [SerializeField] private Assets.Tower.Tower towerPrefab;

    [SerializeField] private bool isPlaceable;

    private Assets.PathFinding.GridManager gridManager;
    private Assets.PathFinding.PathFinder pathFinder;
    private Vector2Int coordinates = new Vector2Int();
    public bool IsPlaceable {  get { return isPlaceable; } }

    private void Awake()
    {
        gridManager = FindObjectOfType<Assets.PathFinding.GridManager>();
        pathFinder  = FindObjectOfType<Assets.PathFinding.PathFinder>();
    }

    private void Start() 
    {
        if(gridManager)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates); 
            }
        }
    }
 
    private void OnMouseUp() 
    {        
        if(gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates) && !Assets.PauseMenu.PauseMenu.isPaused)
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
}
