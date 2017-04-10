using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TeamCreator : ScriptableWizard {
    public int row = 1;
    public int col = 1;
    public float r = 0.8f;
    public string tag = "unit";
    public GameObject prefab;

    [MenuItem("Tools/Create Team")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<TeamCreator>("Create Team", "create");
    }

    void OnWizardCreate()
    {
        if (r < 0 || row <= 0 || col <= 0 || prefab == null)
        {
            EditorUtility.DisplayDialog("Error", "param is invalidate", "ok");
        }
        else
        {
            GameObject[,] units = new GameObject[row, col];
            GameObject teamGo = new GameObject("team");
            for (int i = 0; i < units.GetLength(0); i++)
            {
                for (int j = 0; j < units.GetLength(1); j++)
                {
                    units[i, j] = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    units[i, j].tag = tag;
                    units[i, j].GetComponent<SpriteRenderer>().color = tag == "BlueTeam" ? Color.blue : Color.red;
                    units[i, j].transform.parent = teamGo.transform;
                    units[i, j].transform.position = new Vector3(j * r, -i * r, 0);
                }
            }
            for (int i = 0; i < units.GetLength(1) - 1; i++)
            {
                DistanceJoint2D dj2d = units[0, i].AddComponent<DistanceJoint2D>();
                dj2d.distance = r;
                dj2d.connectedBody = units[0, i + 1].GetComponent<Rigidbody2D>();
            }
            for (int i = 1; i < units.GetLength(0); i++)
            {
                for (int j = 0; j < units.GetLength(1); j++)
                {
                    DistanceJoint2D dj2d = units[i, j].AddComponent<DistanceJoint2D>();
                    dj2d.distance = r;
                    dj2d.connectedBody = units[i - 1, j].GetComponent<Rigidbody2D>();
                }
            }
        }
    }

    void OnWizardUpdate()
    {
        if (prefab == null)
        {
            prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/Unit.prefab", typeof(GameObject));
            Debug.LogFormat("prefab {0}", (prefab == null));
        }
    }

    void OnWizardOtherButton()
    {
    }
}
