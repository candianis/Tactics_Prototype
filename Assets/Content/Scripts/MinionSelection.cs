using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSelection : MonoBehaviour
{

    public RectTransform selectionBox;
    public LayerMask minionLayerMask;
    private List<Minion> selectedMinionsLits = new List<Minion>();
    private Camera cam;
    private PlayerMinions player;

    private Minion minion;
    public Vector3 originPos;

    public Vector2Int cellID;

    private void Awake() {
        cam = Camera.main;
        player = gameObject.GetComponent<PlayerMinions>();

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            VisualToggleSelection(false);
            selectedMinionsLits = new List<Minion>();

            Select(Input.mousePosition);
        }


    }

    void Select(Vector2 screenPos) {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, minionLayerMask)) {
            minion = hit.collider.GetComponent<Minion>();
            if (player.IsMyMinions(minion)) {
                selectedMinionsLits.Add(minion);
                minion.ToggleSelectionVisual(true);

            }
        }

    }





    void VisualToggleSelection(bool selected) {
        foreach (Minion minion in selectedMinionsLits) {
            minion.ToggleSelectionVisual(selected);
        }
    }

    public bool HasMinionsSelected() {
        return selectedMinionsLits.Count > 0 ? true : false;
    }

    public Minion[] GetSelectedUnits() {
        return selectedMinionsLits.ToArray();
    }
}
