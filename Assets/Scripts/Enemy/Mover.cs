using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private GameObject target;
	private PlayerController playerController;
	private Rigidbody2D rb2d;
	public float speed, attack;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> (); 
		target = GameObject.FindGameObjectWithTag ("Player");
		playerController = target.GetComponent<PlayerController> ();

		rb2d.velocity = (target.transform.position - rb2d.transform.position).normalized * speed;
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
		if (otherObject.CompareTag ("Player")) {
			makeDamage (attack);
		}
	}

	void makeDamage (float damage) {
		playerController.loseHealth (damage);
	}


}
