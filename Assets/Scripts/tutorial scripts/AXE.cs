using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AXE : MonoBehaviour {

	public GameObject target;
	private Rigidbody2D rb2d;
	public float speed;
	bool flag;
	private float firstTime;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> (); 
		target = GameObject.FindGameObjectWithTag ("Player");

		rb2d.velocity = (target.transform.position - rb2d.transform.position).normalized * speed;
		rb2d.AddTorque (-450);

		flag = true;
		firstTime = 10000f;
	}

	// Update is called once per frame
	void Update () {
		toNextScene ();	
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		if (otherObject.CompareTag ("Player") || otherObject.CompareTag ("Edge")) {
			Destroy(gameObject);
		}

		if (otherObject.CompareTag ("Trail")) {	
			Destroy(gameObject);
			if (flag) {
				firstTime = Time.time;
				flag = false;
			}
			toNextScene ();
		}
	}


	void toNextScene() {		
		if (Time.time - firstTime >= 5f) {
			SceneManager.LoadScene ("openning");
		}
	}

}
