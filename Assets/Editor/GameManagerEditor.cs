using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    protected GameManager gameManager;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        gameManager = (GameManager)target;

        if(GUILayout.Button("Create Grid"))
        {
            gameManager.CreateGrid();
        }

        if(GUILayout.Button("Clean Grid"))
        {
            gameManager.CleanGrid();
        }
    }
}
