using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    public GameObject selectionVisual;
    private NavMeshAgent navAgent;

    private void Awake() {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void ToggleSelectionVisual(bool selected) {
        selectionVisual.SetActive(selected);
    }

    public void MoveToPosition(Vector3 pos) {
        navAgent.isStopped = false;
        navAgent.SetDestination(pos);
    }

}
