using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class NewChess : ScriptableWizard {
    public int row = 10;
    public int col = 10;

    [MenuItem("Tools/NewChess")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<NewChess>("Create Chess", "create");
    }

    void OnWizardCreate()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        GameObject mapGo = new GameObject("Map");
        Map map = mapGo.AddComponent<Map>();
        Camera.main.orthographic = false;
        Camera.main.transform.position = new Vector3(0, 5, -5);
        Camera.main.transform.Rotate(45, 0, 0);
        Camera.main.gameObject.AddComponent<CameraCtrl>();
        GameObject lightGo = new GameObject("Light");
        Light light = lightGo.AddComponent<Light>();
        light.type = LightType.Directional;
        light.color = Color.white;
        light.transform.position = new Vector3(-1, 1, 1);
        light.transform.LookAt(Vector3.zero);
        light.shadows = LightShadows.Soft;
        light.shadowStrength = 0.5f;
        //GameObject.CreatePrimitive(PrimitiveType.Quad);
        map.pieces = new GameObject[row * col];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Quad);
                map.pieces[i * col + j] = piece;
                MapPiece mp = piece.AddComponent<MapPiece>();
                mp.x = j;
                mp.y = i;
                piece.tag = "MapPiece";
                piece.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", new Color(0, 1, 0));
                piece.transform.localScale = new Vector3(0.9f, 0.9f, 1);
                piece.transform.Rotate(90, 0, 0);
                piece.transform.position = new Vector3(-col / 2.0f + j + 0.5f, 0, -row / 2.0f + i + 0.5f);
                piece.transform.parent = mapGo.transform;
            }
        }
        /*
        for (int i = 0; i <= row; i++)
        {
            GameObject line = new GameObject("line");
            line.transform.parent = mapGo.transform;
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
            line.transform.parent = mapGo.transform;
            LineRenderer lr = line.AddComponent<LineRenderer>();
            Debug.Log(lr);
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.numPositions = 2;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, new Vector3(-col / 2.0f + i, 0, row / 2.0f));
            lr.SetPosition(1, new Vector3(-col / 2.0f + i, 0, -row / 2.0f));
        }
        */
    }
}
