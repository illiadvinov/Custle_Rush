using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.PathFinding
{
    public class PathFinder : MonoBehaviour
{
    public Vector2Int StartCoordinates {get{return startCoordinates;} set{startCoordinates = value;} } 
    public Vector2Int EndCoordinates {get {return endCoordinates;} set{endCoordinates = value;} }
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int endCoordinates;
 

    private Vector2Int[] directions = {Vector2Int.right, Vector2Int.up, Vector2Int.down, Vector2Int.left};
    private Dictionary<Vector2Int, Assets.PathFinding.Node> grid; 
    private Dictionary<Vector2Int, Assets.PathFinding.Node> explored = new Dictionary<Vector2Int, Assets.PathFinding.Node>();
    private Queue<Assets.PathFinding.Node> frontier = new Queue<Assets.PathFinding.Node>();

    private Assets.PathFinding.Node startNode;
    private Assets.PathFinding.Node endNode;
    private Assets.PathFinding.Node currentSearchNode;
    private Assets.PathFinding.GridManager gridManager;

    private void Awake() 
    {
        gridManager = FindObjectOfType<Assets.PathFinding.GridManager>();
        if(gridManager != null) 
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            endNode = grid[endCoordinates];
        }


    }

    private void Start() 
    {
       GetNewPath();
    }

    public List<Assets.PathFinding.Node> GetNewPath()
    {
        gridManager.ResetNode();
        BreadthFirstSearch(startCoordinates);
        return BuildPath();
    } 
    public List<Assets.PathFinding.Node> GetNewPath(Vector2Int coord)
    {
        gridManager.ResetNode();
        BreadthFirstSearch(coord);
        return BuildPath();
    } 

    private void ExploreNeighbours()
    {
        List<Assets.PathFinding.Node> neighbours = new List<Assets.PathFinding.Node>();
        foreach(Vector2Int direction in directions)
        {
            
            Vector2Int neighbourCoordinates = currentSearchNode.coordinates + direction;

          
            if(grid.ContainsKey(neighbourCoordinates)) neighbours.Add(grid[neighbourCoordinates]);
        }

        foreach(Assets.PathFinding.Node neighbour in neighbours)
        {
            if(!explored.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                neighbour.connectedTo = currentSearchNode;
                explored.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
                
        }

        
    }

    private void BreadthFirstSearch(Vector2Int coord)
    {
       startNode.isWalkable = true;
       endNode.isWalkable = true;

       frontier.Clear();
       explored.Clear();

       bool isRunning = true;

       frontier.Enqueue(grid[coord]); 
       explored.Add(coord, grid[coord]); 

       while(frontier.Count > 0 && isRunning)
       {
           currentSearchNode = frontier.Dequeue(); 
           currentSearchNode.isExplored = true;
           ExploreNeighbours();
           if(currentSearchNode.coordinates == endCoordinates) isRunning = false; 
       }
       
    }

    private List<Assets.PathFinding.Node> BuildPath()
    {
        List<Assets.PathFinding.Node> path = new List<Assets.PathFinding.Node>(); 
        Assets.PathFinding.Node currentNode = endNode;
        currentNode.isPath = true;
        path.Add(currentNode);

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;   
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coord)
    {
        if(grid.ContainsKey(coord)) 
        {
            bool previousState = grid[coord].isWalkable;
            
            grid[coord].isWalkable = false;
            List<Assets.PathFinding.Node> newPath = GetNewPath(coord);
            grid[coord].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath(coord);
                return true;
            }
        }
    
       return false;
    }
     
    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
}
