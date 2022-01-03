using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Environment
{
    [ExecuteAlways]
    [RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.black;
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, 0f, 0f);

    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private Assets.PathFinding.GridManager gridManager;
 
    
    private void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        gridManager = FindObjectOfType<Assets.PathFinding.GridManager>();
        DisplayCoordinates();
    }

    private void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void SetLabelColor()
    {
       if(gridManager == null)  return;

        Assets.PathFinding.Node node = gridManager.GetNode(coordinates);
       
       if(node == null)  return;
       
       if(!node.isWalkable) 
       {
            label.color = blockedColor;
       } 

       else if(node.isPath)
       {
           label.color = pathColor;
       }   
       
       else if(node.isExplored) 
       {
           label.color = exploredColor;
       }  
       
       else 
       {
            label.color = defaultColor;
       }
    }

    private void DisplayCoordinates()
    {
        if(gridManager == null) return;

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.WorldGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.WorldGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}

}