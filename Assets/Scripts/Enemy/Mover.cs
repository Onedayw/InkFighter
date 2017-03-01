using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private GameObject target;
	private PlayerController playerController;
	private Rigidbody2D rb2d;
	public int speed, attack;
	public float degreeError;
	public bool rotating;
	private Animator anim;

	private static float arrowExplodeDelay = 0.4f;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> (); 
		target = GameObject.FindGameObjectWithTag ("Player");
		playerController = target.GetComponent<PlayerController> ();
		anim = GetComponent<Animator> ();

		rb2d.velocity = (target.transform.position - rb2d.transform.position).normalized * speed;
		float error = Random.Range (-degreeError, degreeError);
		rb2d.velocity = Quaternion.Euler (0, 0, error) * rb2d.velocity;

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
		if (this.CompareTag ("EnemyMover")) {
			if (otherObject.CompareTag ("Player") || otherObject.CompareTag ("Edge")) {
				rb2d.velocity = new Vector3 (0, 0, 0);
				if (anim != null) anim.SetBool ("exploded", true);
				Destroy(gameObject, arrowExplodeDelay);
			}
			if (otherObject.CompareTag ("Player")) {
				makeDamage (attack);
			}
			if (otherObject.CompareTag ("Trail")) {
				if (rb2d != null) rb2d.velocity = -(rb2d.velocity);
				float reflectDegreeError = playerController.getReflectDegreeError();
				float error = Random.Range (-reflectDegreeError, reflectDegreeError);
				rb2d.velocity = Quaternion.Euler (0, 0, error) * rb2d.velocity;
				this.tag = "PlayerMover";
				this.transform.localScale *= -1;
			}
		} else if (this.CompareTag ("PlayerMover")) {
			if (otherObject.CompareTag ("Enemy") || otherObject.CompareTag ("Edge")) {
				Destroy(gameObject);
			}
			if (otherObject.CompareTag ("Enemy")) {
				Enemy enemy = otherObject.GetComponent<Enemy> ();
				enemy.takeDamage (attack);
			}
		}
	}

	void makeDamage (int damage) {
		playerController.loseHealth (damage);
	}


}
