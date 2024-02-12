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

    private Vector2 boxStartPos;

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
            boxStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0)) {
            RealeaseSelection();
        }
        if (Input.GetMouseButton(0)) {
            UpdateSelectionBox(Input.mousePosition);
        }

    }

    void UpdateSelectionBox(Vector2 curMousePos) {
        if (!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }
        float width = curMousePos.x - boxStartPos.x;
        float height = curMousePos.y - boxStartPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = boxStartPos + new Vector2(width / 2, height / 2);
    }

    void Select(Vector2 screenPos) {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, minionLayerMask)) {
            Minion minion = hit.collider.GetComponent<Minion>();
            if (player.IsMyMinions(minion)) {
                selectedMinionsLits.Add(minion);
                minion.ToggleSelectionVisual(true);
            }
        }
    }
    void RealeaseSelection() {
        selectionBox.gameObject.SetActive(false);
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        foreach (Minion minion in player.minions) {
            Vector3 screenPos = cam.WorldToScreenPoint(minion.transform.position);
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y) {
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
