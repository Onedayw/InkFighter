using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public int speed, attack, startingHealth;
	public Text MoneyText;
	public Text WinText;
	public VitualJoystick moveJoystick;
	public MenuScript menuScript;
	public Image fullHealth;
	public Image damagedHealth;
	public Image BGflash;
	public bool inBossArea;
	public Collider2D bossArea;
	public GameObject circleMover;
	public GameObject shadow;
	public GameObject finger;

	private Animator anim;
	private int currentHealth;
	private float damageTakenTime;
	private float healingInterval = 1.0f;
	private const float hurtTime = 0.1f;
	private bool isHurt;
	private bool faceRight;
	private int money;
	private float inkRange;
	private int selfHealingRate;
	private bool hasCircleSkill = true;


	void Start () {
		anim = GetComponent <Animator> ();
		attack = 10;
		currentHealth = startingHealth;
		isHurt = false;
		faceRight = false;
		BGflash.enabled = false;
		inBossArea = false;
		money = 0;
		inkRange = finger.GetComponent<AsteroidScript> ().getInkRange();
		selfHealingRate = 1;
	}

	void Update () {
		// Handle player position change
		inkRange = finger.GetComponent<AsteroidScript> ().getInkRange();
		float scale = inkRange * 100 / 50;
		shadow.transform.localScale = new Vector3 (scale, scale, scale);
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical);
		if (moveJoystick.InputDirection != Vector3.zero) {
			movement = moveJoystick.InputDirection;
		}

		setMoveAnimation (movement.x, movement.y);

		faceMovingDirection (movement.x);
		transform.Translate(movement * speed * Time.deltaTime, Space.World);

		if (isHurt) {
			if (Time.time > damageTakenTime + hurtTime) {
				isHurt = false;
			}
		}

		updateHealth ();
		SelfHealing ();
		updateMoney ();
		//check if player is in boss's area
		//Debug.Log(bossArea.bounds.extents.x+" "+bossArea.bounds.extents.y+" "+bossArea.bounds.extents.z);
		inBossArea=bossArea.bounds.Contains(this.transform.position);
		//		Debug.Log (this.transform.position);
		//Debug.Log (inBossArea);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("EnemyMover")) {
			loseHealth (5); //todo:losehealth(other.damage)
			damageTakenTime = Time.time;
		}
	}

	void updateMoney () {
		MoneyText.text = "Ink: " + money.ToString();
		//	if (count >= 12) {
		//		WinText.text = "You Win!";
		//	}
	}

	void setMoveAnimation(float x, float y) {
		if (x != 0 || y != 0) {
			anim.SetBool ("isRunning", true);
		} else {
			anim.SetBool ("isRunning", false);
		}
	}

	void updateHealth() {
		if (currentHealth >= startingHealth) {
			fullHealth.enabled = true;
			damagedHealth.enabled = false;
		} else {
			fullHealth.enabled = false;
			damagedHealth.enabled = true;
			damagedHealth.transform.localScale = new Vector3 ((float) currentHealth / startingHealth, 1, 1);
		}
	}

	// Health decrease caused by attach from enemies or traps
	public void loseHealth(int damage){
		if (!isHurt) {
			if (currentHealth > damage) {
				StartCoroutine(BGflashing());
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
			currentHealth -= damage;
			damageTakenTime = Time.time;
			return true;
		}
		else {
			//todo:health bar blink to indicate insufficient health
			return false;
		}
	}

	public void addMoney(int money) {
		this.money += money;
	}

	public void deductMoney(int money) {
		this.money -= money;
	}

	public int getMoney() {
		return this.money;
	}

	void SelfHealing() {
		if (currentHealth < startingHealth && Time.time > damageTakenTime + (healingInterval)) {
			currentHealth += selfHealingRate;
		}
	}

	public void boostSelfHealingRate () {
		selfHealingRate = selfHealingRate * 2;
	}

	public void boostAttack () {
		attack = attack * 2;
	}

	public int getAttack() {
		return attack;
	}

	public void boostSpeed () {
		speed = speed * 2;
	}

	void faceMovingDirection(float moveHorizontal) 
	{
		if (moveHorizontal > 0 && !faceRight) {
			Flip ();
		}
		else if (moveHorizontal < 0 && faceRight) {
			Flip ();
		}
	}

	void Flip ()
	{
		faceRight = !faceRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator BGflashing() {
		BGflash.enabled = true;
		yield return new WaitForSeconds(0.05f);
		BGflash.enabled = false;
	}

	public bool getHasCircleSkill () {
		return this.hasCircleSkill;
	}

	public void setHasCircleSkill (bool hasCircleSkill) {
		this.hasCircleSkill = hasCircleSkill;
	}

	public void circleSkill () {
		if (removeHealth (100) && hasCircleSkill) {
			int v = 10;
			int[,] dirs = { { 0, v }, { v, v }, { v, 0 }, { v, -v }, { 0, -v }, { -v, -v }, { -v, 0 }, { -v, v } };
			for (int i = 0; i < 8; i++) {
				GameObject obj = (GameObject)Instantiate (circleMover, transform.position, transform.rotation);
				obj.tag = "PlayerMover";
				obj.GetComponent<Rigidbody2D> ().velocity = new Vector2 (dirs [i, 0], dirs [i, 1]);
			}
		}
	}
}