using UnityEngine;
using System.Collections;

public class fixEnemy : MonoBehaviour {

	void Start() {
		
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) 
	{

		if (other.CompareTag ("Trail")) {
			Destroy(gameObject);
		}
	}
}
