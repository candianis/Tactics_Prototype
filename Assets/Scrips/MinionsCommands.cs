using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsCommands : MonoBehaviour
{
    public LayerMask layerMask;

    private MinionSelection MinionSelection;
    private Camera cam;

    private void Awake() {
        MinionSelection = GetComponent<MinionSelection>();
        cam = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1) && MinionSelection.HasMinionsSelected()){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Minion[] selectedMinions = MinionSelection.GetSelectedUnits();
            if (Physics.Raycast(ray, out hit, 100, layerMask)) {
                if (hit.collider.CompareTag("Ground")) {
                    MinionsMovesToPos(hit.point, selectedMinions);
                }
            }
        }
    }
    void MinionsMovesToPos(Vector3 movepos, Minion[] minion) {
        for (int x = 0; x < minion.Length; x++) {
            minion[x].MoveToPosition(movepos);
        }
    }
}
