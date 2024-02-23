using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsCommands : MonoBehaviour
{
    public LayerMask layerMask;
    public Vector2Int nextCellID;
    public int moveLimint;
    public bool playerTurn = true;

    private MinionSelection minionSelection;
    private Camera cam;
    private bool alradySpawn = false;

    private void Awake()
    {

        minionSelection = GetComponent<MinionSelection>();
        cam = Camera.main;
    }

    private void Update()
    {

        if (minionSelection != null && Input.GetMouseButton(0) && !alradySpawn) {

            PlayerMinions.instance.MinionSpawnV(CellCheck());
            Debug.Log(CellCheck());
            minionSelection.originPos = CellCheck();
            minionSelection.cellID = nextCellID;
            alradySpawn = true;

        }

        if (Input.GetMouseButtonDown(1) && minionSelection.HasMinionsSelected() && playerTurn)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Minion[] selectedMinions = minionSelection.GetSelectedUnits();
            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    Vector3 cellPos = CellCheck();

                    if (cellPos != Vector3.zero)
                    {

                        minionSelection.originPos = cellPos;
                        MinionsMovesToPos(cellPos, selectedMinions);
                        playerTurn = false;

                    }
                }
            }
        }
    }

    public Vector3 CellCheck()
    {
        RaycastHit hit;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.TryGetComponent(out GridCell gridCell))
            {
                Debug.Log(gridCell.name);
                nextCellID = gridCell.gridID;

                if (!alradySpawn)
                {
                    return gridCell.transform.position;
                }

                if (nextCellID.x == minionSelection.cellID.x || nextCellID.y == minionSelection.cellID.y)
                {
                    
                    if(nextCellID.x != minionSelection.cellID.x)
                    {
                        int xdiference= Mathf.Abs(minionSelection.cellID.x - nextCellID.x);
                        int moveAmount = xdiference;

                        if(moveAmount > moveLimint) 
                        {
                            return Vector3.zero;
                        }
                    }

                    if (nextCellID.y != minionSelection.cellID.y)
                    {
                        int ydiference = Mathf.Abs(minionSelection.cellID.y - nextCellID.y);
                        int moveAmount = ydiference;
                        if(moveAmount > moveLimint) 
                        {
                            return Vector3.zero;
                        }
                    }

                    minionSelection.cellID = nextCellID;
                    return gridCell.transform.position;


                }

                
            }
        }
        return Vector3.zero;
    }


    public void MinionsMovesToPos(Vector3 targetPosition, Minion[] minions)
    {
        for (int x = 0; x < minions.Length; x++)
        {
            minions[x].MoveToPosition(targetPosition);
        }
    }

}
