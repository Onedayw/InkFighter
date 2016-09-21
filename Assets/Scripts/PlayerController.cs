﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;

	public int count;

	public Text CountText;
	public Text WinText;

	public VitualJoystick moveJoystick;

	private Rigidbody2D rb2d;

	public Image fullHealth;
	public Image damagedHealth;
	private float startingHealth = 100;
	private float currentHealth;
	private bool takenDamage;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		count = 0;
		WinText.text = "";
		TextUpdate ();
		takenDamage = false;
		currentHealth = startingHealth;
	}

	void Update() {
		updateHealth ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical);

		if (moveJoystick.InputDirection != Vector3.zero) {
			movement = moveJoystick.InputDirection;
		}

		rb2d.AddForce (movement * speed);
		if (currentHealth == 0) {
			Destroy (gameObject);
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Enemy")) {
			//Destroy (other.gameObject);
			count++;
			TextUpdate ();
		} else if (other.gameObject.CompareTag ("EnemyMover")) {
			currentHealth -= 5;
		}
	}

	void TextUpdate () {
		CountText.text = "Count: " + count.ToString();
		if (count >= 12) {
			WinText.text = "You Win!";
		}
	}

	void updateHealth() {
		if (currentHealth == startingHealth) {
			fullHealth.enabled = true;
			damagedHealth.enabled = false;
		} else {
			fullHealth.enabled = false;
			damagedHealth.enabled = true;
			damagedHealth.transform.localScale = new Vector3 (currentHealth / startingHealth, 1, 1);
		}
	}

}
