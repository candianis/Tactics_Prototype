using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsCommands : MonoBehaviour
{
    public LayerMask layerMask;

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
        
        if (minionSelection != null && Input.GetMouseButton(0) && !alradySpawn)  {
          
            PlayerMinions.instance.MinionSpawnV(CellCheck());
            Debug.Log(CellCheck());
     
            alradySpawn = true;
           
        }
        
        if (Input.GetMouseButtonDown(1) && minionSelection.HasMinionsSelected())
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
                        MinionsMovesToPos(cellPos, selectedMinions);

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
               // Debug.Log(gridCell.name);
                return gridCell.transform.position;

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
