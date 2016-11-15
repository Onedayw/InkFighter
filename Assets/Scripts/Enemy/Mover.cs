using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private GameObject target;
	private PlayerController playerController;
	private Rigidbody2D rb2d;
	public int speed, attack;
	public bool rotating;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> (); 
		target = GameObject.FindGameObjectWithTag ("Player");
		playerController = target.GetComponent<PlayerController> ();

		rb2d.velocity = (target.transform.position - rb2d.transform.position).normalized * speed;
		if (rotating) {
			rb2d.AddTorque (-450);
		} else {
			Vector3 from = new Vector3 (-5, -1, 0);
			Vector3 to = target.transform.position - this.transform.position;
			float dotProduct = from.x * to.x + from.y * to.y + from.z * to.z;
			float v = from.x * to.y - from.y * to.x;
			float angel = Mathf.Atan2 (v, dotProduct) * 180 / Mathf.PI;
			this.transform.rotation = Quaternion.AngleAxis (angel, Vector3.forward);
		}
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

	void makeDamage (int damage) {
		playerController.loseHealth (damage);
	}


}
