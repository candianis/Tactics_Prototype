using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsDoors : MonoBehaviour
{
    public float Keys =0;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Debug.Log("choco con llave");
            Keys++;
            GameManager.instance.keysAmount = (int)Keys;
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Door") && Keys > 0)
        {
            Debug.Log("choco con Puerta");

            Destroy(other.gameObject);
            Keys--;
            GameManager.instance.keysAmount = (int)Keys;
        }
    }

}
