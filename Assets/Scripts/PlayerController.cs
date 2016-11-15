using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public int speed, attack, startingHealth;
	public Text CountText;
	public Text WinText;
	public VitualJoystick moveJoystick;
	public GameObject attackRange;
	public GameObject inkpoint;
	public MenuScript menuScript;
	public Slider healthSlider;
	public Image fullHealth;
	public Image damagedHealth;

	private Rigidbody2D rb2d;
	private int currentHealth;
	private float damageTakenTime;
	private float healingInterval = 1.0f;
	private const float hurtTime = 0.1f;
	private float selfHealRepeatTime = 20f;
	private bool isHurt;


	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		//count = 0;
		attack = 10;
		currentHealth = startingHealth;
		isHurt = false;
	}

<<<<<<< HEAD
	void Update () {
		if (isHurt) {
			if (Time.time > damageTakenTime + hurtTime) {
				isHurt = false;
			}
		}
	}
=======
    void Update () {
        if (isHurt) {
            if (Time.time > damageTakenTime + hurtTime) {
                isHurt = false;
            }
        }

		// Handle health change
		updateHealth ();
		SelfHealing();
    }
>>>>>>> origin/master

	void FixedUpdate () {
		// Handle player position change
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical);
		if (moveJoystick.InputDirection != Vector3.zero) {
			movement = moveJoystick.InputDirection;
		}
		//rb2d.AddForce (movement * speed);
<<<<<<< HEAD
		transform.Translate(movement * speed / 10);
		// Handle health change
		updateHealth ();
		SelfHealing ();
		Debug.Log (currentHealth);
=======
        transform.Translate(movement*speed);
		//InvokeRepeating ("SelfHealing", 0, selfHealRepeatTime);
>>>>>>> origin/master
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("EnemyMover")) {
			loseHealth (5);//todo:losehealth(other.damage)
			damageTakenTime = Time.time;
		}
	}

	//void TextUpdate () {
	//	CountText.text = "Count: " + count.ToString();
	//	if (count >= 12) {
	//		WinText.text = "You Win!";
	//	}
	//}

	void updateHealth() {
		if (currentHealth >= startingHealth) {
			fullHealth.enabled = true;
			damagedHealth.enabled = false;
		} else {
			fullHealth.enabled = false;
			damagedHealth.enabled = true;
			damagedHealth.transform.localScale = new Vector3 ((float)currentHealth / startingHealth, 1, 1);
		}
	}

	// Health decrease caused by attach from enemies or traps
	public void loseHealth(int damage){
		if (!isHurt) {
			if (currentHealth > damage) {
				currentHealth -= damage;
				damageTakenTime = Time.time;
				isHurt = true;
			}
			else {
				currentHealth = 0;
				SceneManager.LoadScene("GameOver");
			}
		}

	}

	// Health decrease caused by drawing & skills
	// Returns true if player has sufficient health, else return false
	public bool removeHealth(int damage){
		if (currentHealth > damage) {
			currentHealth -= 1;
			damageTakenTime = Time.time;
			return true;
		}
		else {
			//todo:health bar blink to indicate insufficient health
			return false;
		}
	}

	void SelfHealing() {
		if (currentHealth < startingHealth && Time.time > damageTakenTime + (healingInterval)) {
			currentHealth += 1;
		}
	}

	public int getAttack() {
		return attack;
	}


}
