using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NUnit : MonoBehaviour {
    public float speed = 1;

	// Use this for initialization
	void Start () {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = rb2d.transform.up.normalized * speed;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != gameObject.tag)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up.normalized * 0.2f);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
