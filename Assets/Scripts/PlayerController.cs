using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public int count;
	public Text CountText;
	public Text WinText;
	public VitualJoystick moveJoystick;
	public GameObject attackRange;
	public GameObject inkpoint;

	public Slider healthSlider;
	private Rigidbody2D rb2d;
	public Image fullHealth;
	public Image damagedHealth;
	private float startingHealth = 1000;
	private float currentHealth;
	private float damageTakenTime;
	private float selfHealRepeatTime = 20.0f;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		count = 0;
		WinText.text = "";
		TextUpdate ();
		currentHealth = startingHealth;
	}

	void Update() {
		//attackRange.transform.position = this.transform.position;
		updateHealth ();
		InvokeRepeating ("SelfHealing", 0, selfHealRepeatTime);
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
		/*if (other.gameObject.CompareTag("Enemy")) {
			//Destroy (other.gameObject);
			count++;
			TextUpdate ();
		} else*/
		if (other.gameObject.CompareTag ("EnemyMover")) {
			loseHealth (5);//todo:losehealth(other.damage)
			damageTakenTime = Time.time;
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
		if (currentHealth <= 0.0) {
			SceneManager.LoadScene ("GameOver");
		}
	}

	public void loseHealth(int damage){
		if (currentHealth > damage) {
			currentHealth = currentHealth - damage;
			damageTakenTime = Time.time;
		} 
		else {
			currentHealth = 0;
		}


	}

	public bool removeHealth(int damage){
		//for health decreasecaused by drawing & skills
		//returns true if player has sufficient health, else return false
		if (currentHealth > damage) {
			currentHealth = currentHealth - 1;
			damageTakenTime = Time.time;
			return true;
		}
		else{
			//todo:health bar blink to indicate insufficient health
			return false;
		}
	}

	void SelfHealing() {
		if (currentHealth < startingHealth && Time.time > damageTakenTime + (2.0f)) {
			currentHealth += 0.25f;
		}
	}
}
