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
    GameState currentState;
    [SerializeField]
    PlayerMinion player;

    [SerializeField]
    GameObject gridCellPrefab;

    [SerializeField]
    Material availableMat;
    Material obstacleMat;
    

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

        currentState = GameState.INIT;
    }

    void Update()
    {
        DecisionManager();
    }

    void DecisionManager()
    {
        if (player.lives <= 0)
            currentState = GameState.GAMEOVER;


    }

    public void CreateGrid()
    {
        for(float y = 0.5f; y < gridHeight; y++)
            for(float x = 0.5f; x < gridWidth; x++)
            {
                GameObject goCell = Instantiate(gridCellPrefab);
                goCell.transform.localPosition = new Vector3(x, -1.4f, y);
                goCell.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                goCell.transform.localRotation = Quaternion.identity;
                goCell.transform.SetParent(this.transform);
                goCell.name = "Cell(" + x + "," + y + ")";

                if(goCell.TryGetComponent(out GridCell gridCell))
                {
                    gridCell.position = new Vector3(x, y, 0);
                    gridCell.type = GridType.WALKABLE;
                }

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
