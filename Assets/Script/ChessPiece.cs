using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour {
    public int x;
    public int y;
    public int speed = 1;
    public int vx;
    public int vy;

    public List<Vector2> path = new List<Vector2>();

	// Use this for initialization
	void Start () {
		
	}

    public void GetDefaultPath()
    {
        path.Clear();
        int x = this.x;
        int y = this.y;
        for (int i = 0; i < speed; i++)
        {
            x += vx;
            y += vy;
            path.Add(new Vector2(x, y));
        }
    }

    public void MoveRound()
    {
        for (int i = 0; i < path.Count; i++)
        {
            if (Map.inst.GetChessPiece(path[i]) != null)
            {
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
