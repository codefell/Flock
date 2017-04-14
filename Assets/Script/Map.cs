using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public int row;
    public int col;
    public GameObject[] pieces;
    public GameObject[] chessPieces;
    //public GameObject[] schedulePieces;
    public static Map inst;
	// Use this for initialization
	void Start () {
        if (inst == null)
        {
            inst = this;
        }
	}

    public void MoveChessPiece(Vector2 from, Vector2 to)
    {
        MoveChessPiece((int)from.x, (int)from.y, (int)to.x, (int)to.y);
    }

    public void MoveChessPiece(int fromX, int fromY, int toX, int toY)
    {
        GameObject fromCp = GetChessPiece(fromX, fromY);
        if (fromCp == null)
        {
            return;
        }
        GameObject toCp = GetChessPiece(toX, toY);
        if (toCp != null)
        {
            throw new System.Exception("error");
        }
        SetChessPiece(fromX, fromY, null);
        SetChessPiece(toX, toY, fromCp);
    }

    public void SetChessPiece(int x, int y, GameObject cp)
    {
        chessPieces[y * col + x] = cp;
    }

    public GameObject GetChessPiece(Vector2 grid)
    {
        return GetChessPiece((int)grid.x, (int)grid.y);
    }

    GameObject GetChessPiece(int x, int y)
    {
        return chessPieces[y * col + x];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
