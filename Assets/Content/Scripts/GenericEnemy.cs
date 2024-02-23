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

    [SerializeField]
    Transform m_target;

    [SerializeField]
    Collider[] perceivedColliders;

    [SerializeField]
    Transform perceptionPosition;

    [SerializeField]
    float detectionRadius;

    void Start()
    {
        cellIndex = 0;
        currentCell = cellPath[0];
    }

    void Update()
    {
        ActionManager();
        DetectionManager();
    }

    private void FixedUpdate()
    {
        perceivedColliders = Physics.OverlapSphere(perceptionPosition.position, detectionRadius);
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
        if (cellIndex >= cellPath.Length)
            cellIndex = 0;

        currentCell = cellPath[cellIndex];
        currentState = EnemyState.WAIT;
    }

    void MoveToNextPosition()
    {
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

    #region Detection Manager

    void DetectionManager()
    {
        PerceptionManager(perceivedColliders);

        if (m_target == null)
            return;

        GameManager.instance.currentState = GameState.GAMEOVER;

    }

    /// <summary>
    /// Target a perceived collider based on a tag
    /// </summary>
    /// <param name="perceivedColliders"></param>
    /// <param name="tagToTarget"></param>
    public void PerceptionManager(Collider[] perceivedColliders)
    {
        m_target = null;
        if (perceivedColliders != null)
        {
            foreach (Collider collider in perceivedColliders)
            {
                if (!collider)
                    continue;

                if(collider.TryGetComponent(out Minion minion) && !GameManager.instance.isCrouching)
                {
                    m_target = minion.transform;
                    break;
                }
            }
        }
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(perceptionPosition.position, detectionRadius);
    }
}