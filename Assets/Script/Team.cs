using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {
    public List<Unit> mBlueTeam;
    public List<Unit> mRedTeam;

    public Vector3 GetCenter(List<Unit> team)
    {
        Vector3 p = Vector3.zero;
        if (team.Count == 0)
        {
            return p;
        }
        foreach (Unit u in team)
        {
            p += u.transform.position;
        }
        p = p / team.Count;
        return p;
    }

    public Vector3 GetTeamCenter(string teamTag)
    {
        List<Unit> team = (teamTag == "BlueTeam" ? mBlueTeam : mRedTeam);
        return GetCenter(team);
    }

	// Use this for initialization
	void Start () {
        GameObject[] blueTeam =  GameObject.FindGameObjectsWithTag("BlueTeam");
        GameObject[] redTeam = GameObject.FindGameObjectsWithTag("RedTeam");
        foreach (GameObject o in blueTeam)
        {
            mBlueTeam.Add(o.GetComponent<Unit>());
        }
        foreach (GameObject o in redTeam)
        {
            mRedTeam.Add(o.GetComponent<Unit>());
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Debug.LogFormat("{0} {1}", GetTeamCenter("BlueTeam"), GetTeamCenter("RedTeam"));
        }
	}
}
