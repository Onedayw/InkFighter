using UnityEngine;
using System.Collections;

public class axeNumber : MonoBehaviour {

	private const float hurtTime = 0f;


	//public int health, attack, speed, vision, money;
	public bool isInBossArea;
	public int fullHealth, attack, speed, vision, money;
	public int health;

	private float damageTakenTime;
	private bool isHurt, seenTarget;

	// private EnemyManager enemyManager;
	private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
	private GameObject target;                           //Transform to attempt to move toward each turn.
	private PlayerController playerController;
	private float alphaLevel;

	private static float enemyDeadTime = 0.4f;

	private Level0Event level0;
	public GameObject EventSystem;

	// Use this for initialization
	void Start () {
		health = fullHealth;
		isHurt = false;
		target = GameObject.FindGameObjectWithTag ("Player");
		playerController = target.GetComponent<PlayerController> ();
		animator = GetComponent<Animator> ();
		level0 = EventSystem.GetComponent<Level0Event>();
	}

	// Update is called once per frame
	void Update () {
		if (isHurt) {
			if (Time.time > damageTakenTime + hurtTime) {
				isHurt = false;
			}
		}
	}

	public bool setSeenTarget() {
		if (isInBossArea) {
			seenTarget = playerController.inBossArea;
		}
		else seenTarget = true;
		return seenTarget;
	}

	public bool getSeenTarget() {
		return seenTarget;
	}

	public GameObject getTarget() {
		return target;
	}

	// Usc this for taking damage
	public int takeDamage(int damage) {
		if (!isHurt) {
			health -= damage;
			if (gameObject.tag == "Enemy") {
				updatecolor ();
			}
			if (health <= 0.0f) {
				die ();
			}
			damageTakenTime = Time.time;
			isHurt = true;
		}
		return health;
	}

	public void updatecolor () {
		alphaLevel = (float) (0.4 + ((float)health / (float)fullHealth) * 0.5);
		// Debug.Log(alphaLevel);
		GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, alphaLevel);
	}

	public void die() {
		animator.SetBool ("isDead", true);
		Destroy(this.gameObject, enemyDeadTime);
		playerController.addMoney (money);
		level0.setAxeNumber ();
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag ("Trail")) {
			takeDamage (playerController.getAttack ());
			//Debug.Log(playerController.getAttack());
		}
		if (other.CompareTag ("PlayerMover")) {
			takeDamage (15);
			//Debug.Log(playerController.getAttack());
		}
	}
}
