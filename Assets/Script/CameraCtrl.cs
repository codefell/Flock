using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {
    public float moveFactor = 0.02f;
    public List<MapPiece> path = new List<MapPiece>();
    public string op = "";

	// Use this for initialization
	void Start () {
	}

    void OnGUI()
    {
        if (Event.current.type == EventType.MouseDrag)
        {
            if (op == "drawPath")
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                Physics.Raycast(ray, out hit);
                if (hit.collider != null && hit.collider.tag == "MapPiece")
                {
                    MapPiece mp = hit.collider.GetComponent<MapPiece>();
                    if (mp != path[path.Count - 1])
                    {
                        if (path.Count > 2)
                        {
                            MapPiece p = path[path.Count - 2];
                            if (Mathf.Abs(mp.x - p.x) == 1
                                && Mathf.Abs(mp.y - p.y) == 1)
                            {
                                path[path.Count-1].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color());
                                path.RemoveAt(path.Count - 1);
                            }
                        }
                        mp.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1f, 0, 0));
                        path.Add(mp);
                    }
                }
            }
            else
            {
                float x = -Event.current.delta.x;
                float z = Event.current.delta.y;
                Vector3 v = new Vector3(x, 0, z);
                v = Camera.main.transform.TransformVector(v);
                v.y = 0;
                Camera.main.transform.position += v * moveFactor;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "ChessPiece")
                {
                    Vector3 pos = hit.collider.transform.position;
                    hit.collider.transform.position = new Vector3(pos.x, 1, pos.z);
                }
                if (hit.collider.tag == "MapPiece")
                {
                    MapPiece mp = hit.collider.GetComponent<MapPiece>();
                    for (int i = 0; i < path.Count; i++)
                    {
                        path[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color());
                    }
                    path.Clear();
                    path.Add(mp);
                    op = "drawPath";
                    hit.collider.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0));
                    //Color c = hit.collider.GetComponent<Renderer>().material.GetColor("_EmissionColor");
                    //Debug.LogFormat("color {0}", c);
                }
            }
            else
            {
                for (int i = 0; i < path.Count; i++)
                {
                    path[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color());
                }
                path.Clear();
                op = "";
            }
        }
	}
}
