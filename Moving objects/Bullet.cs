using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float bulletSpeed = 1.0f;
	private int bulletLife = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * Time.deltaTime*-bulletSpeed;
	}

	void OnTriggerEnter2D(Collider2D col2d){

		if (col2d.tag == "Wall" || col2d.tag == "Player" || col2d.tag == "Die") {
			bulletLife--;
		}
		if (bulletLife <= 0 || col2d.tag == "Destroyer") {
			Destroy (gameObject);
		}
	}
}