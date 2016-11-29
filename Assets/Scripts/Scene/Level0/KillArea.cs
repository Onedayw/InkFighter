using UnityEngine;
using System.Collections;

public class KillArea : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		if (otherObject.CompareTag("Player")) {
			Handheld.PlayFullScreenMovie ("Attack.mp4", Color.black, FullScreenMovieControlMode.Hidden);
			Destroy(this.GetComponent<BoxCollider2D>());
		}
	}
}
