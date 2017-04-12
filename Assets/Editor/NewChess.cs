using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class NewChess : ScriptableWizard {
    public int row = 8;
    public int col = 8;

    [MenuItem("Tools/NewChess")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<NewChess>("Create Chess", "create");
    }

    void OnWizardCreate()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        GameObject map = new GameObject("Map");
        Camera.main.orthographic = false;
        //Camera.main.transform.position = new Vector3(0, 30, -42);
        Camera.main.transform.Rotate(45, 0, 0);
        for (int i = 0; i <= row; i++)
        {
            GameObject line = new GameObject("line");
            line.transform.parent = map.transform;
            LineRenderer lr = line.AddComponent<LineRenderer>();
            Debug.Log(lr);
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.numPositions = 2;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, new Vector3(-col / 2.0f, 0, row / 2.0f - i));
            lr.SetPosition(1, new Vector3(col / 2.0f, 0, row / 2.0f - i));
        }
        for (int i = 0; i <= col; i++)
        {
            GameObject line = new GameObject("line");
            line.transform.parent = map.transform;
            LineRenderer lr = line.AddComponent<LineRenderer>();
            Debug.Log(lr);
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.numPositions = 2;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, new Vector3(-col / 2.0f + i, 0, row / 2.0f));
            lr.SetPosition(1, new Vector3(-col / 2.0f + i, 0, -row / 2.0f));
        }
    }
}
