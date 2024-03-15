using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public enum EnemyState
{
    START,
    INIT,
    MOVE,
    WAIT,
    DEAD
}

public class GenericEnemy : MonoBehaviour
{
    public EnemyState currentState;
    [SerializeField]
    GridCell[] cellPath;
    
    [SerializeField]
    GridCell currentCell;

    [SerializeField, Tooltip("The current cell inside the path")]
    int cellIndex;

    [SerializeField] 
    int speed;

    [SerializeField]
    bool moveBackward;

    void Start()
    {
        cellIndex = 0;
        if(cellPath.Length > 0)
            currentCell = cellPath[0];
    }

    void Update()
    {
        ActionManager();
    }

    #region Action Manager
    void ActionManager()
    {
        switch (currentState)
        {
            case EnemyState.INIT:
                GetNextPosition();
                break;

            case EnemyState.MOVE:
                MoveToNextPosition();
                break;

            case EnemyState.WAIT:

                break;

            case EnemyState.DEAD:
                break;

            default:
                break;
        }
    }

    void GetNextPosition()
    {
        ++cellIndex;
        if (cellPath.Length <= 0)
            return;

        if (cellIndex >= cellPath.Length)
            cellIndex = 0;

        currentCell = cellPath[cellIndex];
        currentState = EnemyState.WAIT;
    }

    void MoveToNextPosition()
    {
        if (currentCell == null)
            return;
        float yPos = transform.position.y;
        Vector3 nextPosition = currentCell.transform.position;
        nextPosition.y = yPos;

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPosition) < 0.125f)
        {
            currentState = EnemyState.INIT;
        }
    }

    #endregion
}