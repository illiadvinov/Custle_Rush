using System.Collections.Generic;
using UnityEngine;

namespace Assets.PathFinding
{
    public class GridManager : MonoBehaviour
{
    public Vector2Int GridSize { set {gridSize = value;} }
    public Dictionary<Vector2Int, Node> Grid {get { return grid; } }
    public int WorldGridSize { get{return worldGridSize; } }
    [SerializeField] private Vector2Int gridSize; 
    private int worldGridSize = 10;
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();


    private void Awake() 
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates)) return grid[coordinates];
        return null;
    }
    public void BlockNode(Vector2Int coord)
    {
        if(grid.ContainsKey(coord)) grid[coord].isWalkable = false;
    }

    public void ResetNode()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;

        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / WorldGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / WorldGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coord)
    {
        Vector3 position = new Vector3();
        position.x = coord.x * WorldGridSize;
        position.z = coord.y * WorldGridSize;

        return position;
    }

    private void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int currentCoordinates = new Vector2Int(x, y);
                Node currentNode = new Node(currentCoordinates, true);
                grid.Add(currentCoordinates, currentNode);
            }
        }
    }
}
}
