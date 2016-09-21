using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;

	public int count;

	public Text CountText;
	public Text WinText;

	public VitualJoystick moveJoystick;

	private Rigidbody2D rb2d;

	public int startingDmageTaken = 0;                            // The amount of damage Player taken
	public int currentDmageTaken;                                   // The current damage Player has taken
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	private bool takenDamage;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		count = 0;
		WinText.text = "";
		TextUpdate ();
		currentDmageTaken = startingDmageTaken;
		takenDamage = false;
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical);

		if (moveJoystick.InputDirection != Vector3.zero) {
			movement = moveJoystick.InputDirection;
		}

		rb2d.AddForce (movement * speed);
		if (healthSlider.value == healthSlider.maxValue) {
			Destroy (gameObject);
		} else if (!takenDamage) {
			healthSlider.value--;
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Enemy")) {
			Destroy (other.gameObject);
			count++;
			TextUpdate ();
		} else if (other.gameObject.CompareTag ("EnemyMover")) {
			healthSlider.value += 5;
			takenDamage = true;
		}
		takenDamage = false;
	}

	void TextUpdate () {
		CountText.text = "Count: " + count.ToString();
		if (count >= 12) {
			WinText.text = "You Win!";
		}
	}

}
