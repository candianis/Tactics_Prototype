using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    INIT,
    GAME,
    GAMEOVER,
    PAUSE
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gridHeight, gridWidth;

    [SerializeField]
    List<GameObject> gridCells;
    [SerializeField]
    PlayerMinion player;

    [SerializeField]
    GameObject gridCellPrefab;

    [SerializeField]
    Material availableMat;
    Material obstacleMat;

    [SerializeField] 
    GenericEnemy[] enemies;
    

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    void Update()
    {
        DecisionManager();
    }

    void DecisionManager()
    {
        //if (player.lives <= 0)
        //    currentState = GameState.GAMEOVER;


    }

    public Vector3 GetGridCell(Vector2Int gridPosition)
    {
        foreach(GameObject cell in gridCells)
        {
            if(!TryGetComponent(out GridCell gridCell))
            {
                continue;
            }

            if (gridCell.gridID.Equals(gridPosition))
            {
                return gridCell.position;
            }
        }

        return Vector3.zero;
    }

    public void CreateGrid()
    {
        for(float y = transform.position. z + 0.5f; y < (transform.position.z + gridHeight); y++)
            for(float x = transform.position.x + 0.5f; x < (transform.position.x + gridWidth); x++)
            {
                GameObject goCell = Instantiate(gridCellPrefab);
                goCell.transform.localPosition = new Vector3(x, -1.4f, y);
                goCell.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                goCell.transform.localRotation = Quaternion.identity;
                goCell.transform.SetParent(this.transform);

                if(goCell.TryGetComponent(out GridCell gridCell))
                {
                    gridCell.position = new Vector3(x, y, 0);
                    gridCell.type = GridType.WALKABLE;
                    gridCell.gridID = new Vector2Int(((int)x), ((int)y));
                }
                goCell.name = "Cell(" + gridCell.gridID.x  + "," + gridCell.gridID.y + ")";

                gridCells.Add(goCell);
            }
    }

    public void CleanGrid()
    {
        if (gridCells.Count <= 0)
            return;

        foreach(GameObject cell in gridCells)
        {
            DestroyImmediate(cell, false);
        }
        gridCells.Clear();
    }
}