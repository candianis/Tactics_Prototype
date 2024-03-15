using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    INIT,
    MENU,
    GAME,
    GAMEOVER,
    PAUSE
}

public enum TurnState
{
    PLAYER_TURN,
    AI_TURN
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gridHeight, gridWidth;
    public float timeToWait;

    [SerializeField]
    List<GameObject> gridCells;
    public GameState currentState;

    [SerializeField] GameObject pauseMenu;

    public TurnState turnState;

    [SerializeField]
    PlayerMinion player;

    [SerializeField]
    GameObject gridCellPrefab;

    [SerializeField]
    Material availableMat;
    Material obstacleMat;

    public GameObject gameOverMenu;

    public bool isCrouching = false;

    [SerializeField]
    GenericEnemy[] enemies;

    public MinionsCommands commands;
    public int keysAmount;
    public TextMeshProUGUI keyText;
    public GameObject crouchingTexture;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        DecisionManager();
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            isCrouching = true;
            commands.moveLimint /= 2;
            crouchingTexture.SetActive(true);
        }
        else
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            isCrouching = false;
            commands.moveLimint *= 2;
            crouchingTexture.SetActive(false);
        }

            //  keyText.text = "Llaves: " + keysAmount;
        PauseGame();
    }

    void DecisionManager()
    {
        if(currentState == GameState.GAMEOVER)
        {
            gameOverMenu.SetActive(true);
            return;
        }

        if (!commands.playerTurn)
        {
            turnState = TurnState.AI_TURN;
        }

        if(turnState == TurnState.AI_TURN)
            StartCoroutine(MoveEnemies());

    }

    IEnumerator MoveEnemies()
    {
        Debug.Log("ai");
        yield return new WaitForSeconds(1.5f);
        if (enemies.Length > 0)
        {
            foreach (GenericEnemy enemy in enemies)
            {
                if(enemy)
                    enemy.currentState = EnemyState.MOVE;
            }
        }
        commands.playerTurn = true;
        yield return new WaitForSeconds(timeToWait);
        turnState = TurnState.PLAYER_TURN;
    }

    #region Grid Logic

    public Vector3 GetGridCell(Vector2Int gridPosition)
    {
        foreach (GameObject cell in gridCells)
        {
            if (!TryGetComponent(out GridCell gridCell))
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
        for (float y = transform.position.z + 0.5f; y < (transform.position.z + gridHeight); y++)
            for (float x = transform.position.x + 0.5f; x < (transform.position.x + gridWidth); x++)
            {
                GameObject goCell = Instantiate(gridCellPrefab);
                goCell.transform.localPosition = new Vector3(x, -1.4f, y);
                goCell.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                goCell.transform.localRotation = Quaternion.identity;
                goCell.transform.SetParent(this.transform);

                if (goCell.TryGetComponent(out GridCell gridCell))
                {
                    gridCell.position = new Vector3(x, y, 0);
                    gridCell.type = GridType.WALKABLE;
                    gridCell.gridID = new Vector2Int(((int)x), ((int)y));
                }
                goCell.name = "Cell(" + gridCell.gridID.x + "," + gridCell.gridID.y + ")";

                gridCells.Add(goCell);
            }
    }

    public void CleanGrid()
    {
        if (gridCells.Count <= 0)
            return;

        foreach (GameObject cell in gridCells)
        {
            DestroyImmediate(cell, false);
        }
        gridCells.Clear();
    }
    #endregion

    public void RestartGame()
    { 
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void PauseGame() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenu.SetActive(true);
            currentState = GameState.PAUSE;
        }
    }

    public void UnpauseGame() {
        currentState = GameState.GAME;
    }
}