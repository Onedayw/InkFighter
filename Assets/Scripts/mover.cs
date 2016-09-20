using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speed;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector3 (3, -3, 0) * speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, -450) * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		if (otherObject.CompareTag ("Player") || otherObject.CompareTag ("Edge")) {
			Destroy(gameObject);
		}
	}


}
