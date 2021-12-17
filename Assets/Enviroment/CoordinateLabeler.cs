using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.black;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0f, 0f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
 
    
    


    void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinates();
    }

    void Update()
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

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
       if(gridManager == null)  return;

       Node node = gridManager.GetNode(coordinates);
       
       if(node == null)  return;
       
       if(!node.isWalkable) 
       {
            label.color = blockedColor;
            //Debug.Log("Blocked color!");
       } 

       else if(node.isPath)
       {
           label.color = pathColor;
           //Debug.Log("Path Color!");
       }   
       
       else if(node.isExplored) 
       {
           label.color = exploredColor;
           //Debug.Log("Explored Color!");
       }  
       
       else 
       {
            label.color = defaultColor;
            //Debug.Log("Default Color!");
       }
    }

    void DisplayCoordinates()
    {
        if(gridManager == null) return;

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.WorldGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.WorldGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
