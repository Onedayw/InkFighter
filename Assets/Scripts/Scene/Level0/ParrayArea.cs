using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParrayArea : MonoBehaviour {

	public GameObject player;
	private PlayerController playerController;
	private int parryNumber;
	public Image counter;

	void Start () {		
		parryNumber = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		playerController = player.GetComponent<PlayerController> ();
	}
	

	void Update () {
		parryNumber = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (parryNumber == 0) {
			counter.enabled = false;
			playerController.speed = 5;
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		if (otherObject.CompareTag("Player")) {
			playerController.speed = 0;
			Handheld.PlayFullScreenMovie ("Parry.mp4", Color.black, FullScreenMovieControlMode.Hidden);
			counter.enabled = true;
			Destroy(this.GetComponent<BoxCollider2D>());
		}

	}
}
