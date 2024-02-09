using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinions : MonoBehaviour
{
    public List<Minion> minions = new List<Minion>();

    public bool IsMyMinions(Minion minion) {
        return minions.Contains(minion);
    }
}
