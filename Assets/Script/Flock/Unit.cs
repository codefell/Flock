using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    Rigidbody2D rb2d;
    public float mConstantForce = 1f;
    private bool engage = false;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}

    public Vector3 GetEnemyTeamCenter()
    {
        string enemyTag = (gameObject.tag == "BlueTeam" ? "RedTeam" : "BlueTeam");
        Team team = Camera.main.GetComponent<Team>();
        return team.GetTeamCenter(enemyTag);
    }

    void OnCollisionEnter2D(Collision2D collider2D)
    {
        if (collider2D.gameObject.tag != gameObject.tag)
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update() {
        Vector2 forceDir = GetEnemyTeamCenter() - transform.position;
        rb2d.velocity = forceDir.normalized * mConstantForce;
    }
}
