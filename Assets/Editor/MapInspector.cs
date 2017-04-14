using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class MapInspector : Editor{
    private string[] modes = new string[] { "View", "RedUnit", "BlueUnit", "Erase" };
    private int currMode = 0;
    private Map mTarget;
    private GameObject mRedUnit;
    private GameObject mBlueUnit;

    void OnEnable()
    {
        mTarget = (Map)target;
        if (mRedUnit == null)
        {
            mRedUnit = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/RedUnit.prefab");
            mBlueUnit = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/BlueUnit.prefab");
        }
    }

    void OnSceneGUI()
    {
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(10f, 10f, 360f, 40f));
        currMode = GUILayout.Toolbar(currMode, modes, GUILayout.ExpandHeight(true));
        GUILayout.EndArea();
        Handles.EndGUI();

        if (Event.current.type == EventType.mouseDown && Event.current.button == 0)
        {
            Vector3 mousePos = Event.current.mousePosition;
            Camera camera = SceneView.currentDrawingSceneView.camera;
            mousePos = Event.current.mousePosition;
            mousePos.y = camera.pixelHeight - mousePos.y;
            Ray ray = camera.ScreenPointToRay(mousePos);
            RaycastHit rayHit = new RaycastHit();
            if (Physics.Raycast(ray, out rayHit, float.MaxValue, 1 << LayerMask.NameToLayer("MapPiece")))
            {
                if (modes[currMode] == "RedUnit")
                {
                    GameObject redUnit = (GameObject)PrefabUtility.InstantiatePrefab(mRedUnit);
                    ChessPiece cp = redUnit.GetComponent<ChessPiece>();
                    MapPiece mp = rayHit.collider.GetComponent<MapPiece>();
                    cp.x = mp.x;
                    cp.y = mp.y;
                    mTarget.chessPieces[mp.y * mTarget.col + mp.x] = redUnit;
                    redUnit.transform.position = rayHit.collider.transform.position + new Vector3(0, 0.2f, 0);
                }
                else if (modes[currMode] == "BlueUnit")
                {
                    GameObject blueUnit = (GameObject)PrefabUtility.InstantiatePrefab(mBlueUnit);
                    ChessPiece cp = blueUnit.GetComponent<ChessPiece>();
                    MapPiece mp = rayHit.collider.GetComponent<MapPiece>();
                    cp.x = mp.x;
                    cp.y = mp.y;
                    mTarget.chessPieces[mp.y * mTarget.col + mp.x] = blueUnit;
                    blueUnit.transform.position = rayHit.collider.transform.position + new Vector3(0, 0.2f, 0);
                }
            }
        }
    }

}
