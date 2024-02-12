using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridType
{
    WALKABLE,
    OBSTACLE,
    SELECTED
}

public class GridCell : MonoBehaviour
{
    public Vector3 position;
    public GridType type;


    private void Update()
    {
        
    }
}
