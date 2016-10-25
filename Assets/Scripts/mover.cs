using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public Transform target;
	private Rigidbody2D rb2d;
	public float speed;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> (); 
		target = GameObject.FindGameObjectWithTag ("Player").transform;

		rb2d.velocity = (target.position - rb2d.transform.position).normalized * speed;
		rb2d.AddTorque (-450);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		if (otherObject.CompareTag ("Player") || otherObject.CompareTag ("Edge") || otherObject.CompareTag ("Trail")) {
			Destroy(gameObject);
		}
	}


}
