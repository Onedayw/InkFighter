using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillArea : MonoBehaviour {

	public Image kill;

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		if (otherObject.CompareTag("Player")) {
			Handheld.PlayFullScreenMovie ("Attack.mp4", Color.black, FullScreenMovieControlMode.Hidden);
			kill.enabled = true;
			Destroy(this.GetComponent<BoxCollider2D>());
		}
	}
}
