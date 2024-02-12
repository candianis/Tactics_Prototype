using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    TURN_START,
    TURN_WAIT,
    TURN_END,
    DEAD
}

public class PlayerMinion : MonoBehaviour
{
    public int lives;
    public int movementLimit;

    public GridCell currentCell;
    public GridCell selectedCell;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectGrid();
    }

    void SelectGrid()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AltRay();
        }
    }

    void AltRay()
    {
        RaycastHit hit;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.transform.TryGetComponent(out GridCell gridCell))
            {
                Debug.Log(gridCell.name);
            }
        }
    }
}