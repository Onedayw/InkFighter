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
	private float startingHealth;
	private float currentHealth;
	private float damageTakenTime;
	private float selfHealRepeatTime = 20f;
	private float attack;
    public MenuScript menuScript;


	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		count = 0;
		WinText.text = "";
		TextUpdate ();
		currentHealth = startingHealth;
		attack = 10f;
		startingHealth = 100f;
		currentHealth = startingHealth;
	}

    void Update () {
	}

	void FixedUpdate () {
		// Handle player position change
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical);
		if (moveJoystick.InputDirection != Vector3.zero) {
			movement = moveJoystick.InputDirection;
		}
		rb2d.AddForce (movement * speed);

		// Handle health change
		updateHealth ();
		InvokeRepeating ("SelfHealing", 0, selfHealRepeatTime);

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("EnemyMover")) {
			loseHealth (5.0f);//todo:losehealth(other.damage)
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
	}

	// Health decrease caused by attach from enemies or traps
	public void loseHealth(float damage){
		if (currentHealth > damage) {
			currentHealth -= damage;
			damageTakenTime = Time.time;
		} 
		else {
			currentHealth = 0.0f;
			SceneManager.LoadScene ("GameOver");
		}
	}

	// Health decrease caused by drawing & skills
	// Returns true if player has sufficient health, else return false
	public bool removeHealth(float damage){
		if (currentHealth > damage) {
			currentHealth = currentHealth - 1.0f;
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

	public float getAttack() {
		return attack;
	}


}
