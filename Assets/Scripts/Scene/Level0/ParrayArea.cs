using UnityEngine;
using System.Collections;

public class ParrayArea : MonoBehaviour {

	public GameObject player;
	private PlayerController playerController;
	private int parryNumber;

	void Start () {		
		parryNumber = GameObject.FindGameObjectsWithTag ("parry").Length;
		playerController = player.GetComponent<PlayerController> ();
	}
	

	void Update () {
		parryNumber = GameObject.FindGameObjectsWithTag ("parry").Length;
		if (parryNumber == 0) {
			playerController.speed = 5;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		if (otherObject.CompareTag("Player")) {
			playerController.speed = 0;
			Handheld.PlayFullScreenMovie ("Parry.mp4", Color.black, FullScreenMovieControlMode.Hidden);
			Destroy(this.GetComponent<BoxCollider2D>());
		}

	}
}
