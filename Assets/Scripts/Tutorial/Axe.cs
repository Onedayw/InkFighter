using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Axe : Mover {

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		if (otherObject.CompareTag ("Player") || otherObject.CompareTag ("Edge")) {
			Destroy(gameObject);
		}

		if (otherObject.CompareTag ("Trail")) {	
			TutorialParry.parryAXE++;
			Destroy(gameObject);
		}
	}
}
