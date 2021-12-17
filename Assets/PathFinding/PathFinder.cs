using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int endCoordinates;
    public Vector2Int StartCoordinates {get{return startCoordinates;}}
    public Vector2Int EndCoordinates{get {return endCoordinates;}}

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.up, Vector2Int.down, Vector2Int.left};
    Dictionary<Vector2Int, Node> grid; //Declaring Dictionary just for comfortability(grid == gridManager.Grid)
    Dictionary<Vector2Int, Node> explored = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    Node startNode;
    Node endNode;
    Node currentSearchNode;
    GridManager gridManager;

    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
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

    public List<Node> GetNewPath()
    {
        gridManager.ResetNode();
        BreadthFirstSearch(startCoordinates);
        return BuildPath();
    } 
    public List<Node> GetNewPath(Vector2Int coord)
    {
        gridManager.ResetNode();
        BreadthFirstSearch(coord);
        return BuildPath();
    } 

    private void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();
        foreach(Vector2Int direction in directions)
        {
            //Searching Neighbour Coodrdinates (F.e cur coord - 1.1, neb coord - 1.2)
            Vector2Int neighbourCoordinates = currentSearchNode.coordinates + direction;

            //If Neighbour Coodrdinate is in our GridSize we will add it
            if(grid.ContainsKey(neighbourCoordinates)) neighbours.Add(grid[neighbourCoordinates]);
        }

        foreach(Node neighbour in neighbours)
        {
            if(!explored.ContainsKey(neighbour.coordinates) && neighbour.isWalkable) // If neighbour coord wasn't explored
            {
                neighbour.connectedTo = currentSearchNode;
                explored.Add(neighbour.coordinates, neighbour);// We're adding neighbour to our explored Dictionary
                frontier.Enqueue(neighbour);// Also we're adding neighbour Node to our Queue
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

       frontier.Enqueue(grid[coord]); // First Element in Queue is startNode
       explored.Add(coord, grid[coord]); // As we added to frontier our startNode, it has been explored

       while(frontier.Count > 0 && isRunning)
       {
           currentSearchNode = frontier.Dequeue(); // Getting what Node is currently  (f.e. is startNode, for another we are looking in ExploreMethod)
           currentSearchNode.isExplored = true;
           ExploreNeighbours();
           if(currentSearchNode.coordinates == endCoordinates) isRunning = false; // If we've reached end, we need to exit from loop
       }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>(); // Return path
        Node currentNode = endNode; // Going from end
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
            List<Node> newPath = GetNewPath(coord);
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
