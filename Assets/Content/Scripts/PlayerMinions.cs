using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerMinions : MonoBehaviour

{
    static public PlayerMinions instance;
    public List<Minion> minions = new List<Minion>();
    public GameObject minionPref;
    public Vector3 spawnMinionPos;
    private void Awake()
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
    public bool IsMyMinions(Minion minion) {
        return minions.Contains(minion);
    }

    public void MinionSpawnV( Vector3 minionPos)
    {
        GameObject spawnMinion = Instantiate(minionPref);
        spawnMinion.transform.position = minionPos;
            
        Minion spawnFirtsMinion = spawnMinion.GetComponent<Minion>();
        minions.Add(spawnFirtsMinion);

        CinemachineVirtualCamera cam = Camera.main.GetComponent<CinemachineVirtualCamera>();
        cam.Follow = spawnMinion.transform;

    }

}
