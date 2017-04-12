using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(World))]
public class WorldInspector: Editor{

    private void OnSceneGUI()
    {
        Handles.BeginGUI();
        if (GUI.Button(new Rect(50, 50, 100, 40), "NewTeam"))
        {
            Debug.Log("NewTeam");
        }
        Handles.EndGUI();
    }

}
