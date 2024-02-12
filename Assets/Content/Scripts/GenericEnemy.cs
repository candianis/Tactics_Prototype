using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    INIT,
    MOVE,
    WAIT,
    DEAD
}

enum LookDirection
{
    UP,
    DOWN,
    RIGHT,
    LEFT
}

public class GenericEnemy : MonoBehaviour
{
    [SerializeField] 
    EnemyState currentState;
    [SerializeField] 
    LookDirection direction;
    [SerializeField] 
    Vector2Int currentPosition;
    [SerializeField] 
    Vector2Int nextPosition;
    [SerializeField] 
    int speed;

    void Start()
    {
        
    }

    void Update()
    {
        ActionManager();   
    }

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
        nextPosition = currentPosition;
        direction = (LookDirection)Random.Range(0, 3);
        switch (direction)
        {
            case LookDirection.UP:
                nextPosition += new Vector2Int(0, speed);
                break;
            case LookDirection.DOWN:
                nextPosition -= new Vector2Int(0, speed);
                break;
            case LookDirection.RIGHT:
                nextPosition += new Vector2Int(speed, 0);
                break;

            case LookDirection.LEFT:
                nextPosition -= new Vector2Int(speed, 0);
                break;
        }
        currentState = EnemyState.WAIT;
    }

    void MoveToNextPosition()
    {
        Vector3 newPosition = new Vector3(nextPosition.x, nextPosition.y, 0);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, newPosition) < 0.125f)
        {
            currentState = EnemyState.WAIT;
            currentPosition = nextPosition;
        }
    }
}
