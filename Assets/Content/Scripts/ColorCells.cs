using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ColorCells : MonoBehaviour
{
    public MinionsCommands commands;
    public int rayLimit;
    private void Start()
    {
        commands = FindObjectOfType<MinionsCommands>();
    }
    private void Update()
    {
        rayLimit = commands.moveLimint;
    }
    public void ChangeCellColor()
    {

        Ray[] rays = new Ray[4]
        {
                new Ray(transform.position + Vector3.down, Vector3.left * 4),
                new Ray(transform.position + Vector3.down, Vector3.back * 4),
                new Ray(transform.position + Vector3.down, Vector3.right * 4),
                new Ray(transform.position + Vector3.down, Vector3.forward * 4)

        };

        for (int i = 0; i < rays.Length; i++)
        {

            RaycastHit[] hit = Physics.RaycastAll(rays[i],rayLimit);

            foreach (RaycastHit hit2 in hit)
            {

                if (hit2.collider.CompareTag("Cell"))
                {

                    Renderer renderer = hit2.collider.GetComponent<Renderer>();

                    if (renderer.material.color == Color.cyan)
                    {

                        renderer.material.color = Color.green;
                    }
                    else
                    {

                        renderer.material.color = Color.cyan;

                    }

                }

            }

        }



    }

}