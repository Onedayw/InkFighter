using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private const float hurtTime = 2f;

	public float health, attack, speed, vision;
	private float damageTakenTime;
	private bool isHurt, seenTarget;

	private EnemyManager enemyManager;
	private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
	private GameObject target;                           //Transform to attempt to move toward each turn.
	private PlayerController playerController;

	// Use this for initialization
	void Start () {
		isHurt = false;
		target = GameObject.FindGameObjectWithTag ("Player");
		playerController = target.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isHurt) {
			if (Time.time > damageTakenTime + hurtTime) {
				isHurt = false;
			}
		}
	}

	public void debug() {
		Debug.Log ("I got it!");
	}

	public bool setSeenTarget() {
		seenTarget = true;
		return seenTarget;
	}

	public bool getSeenTarget() {
		return seenTarget;
	}

	public GameObject getTarget() {
		return target;
	}

	// Usc this for taking damage
	public float takeDamage(float damage) {
		if (!isHurt) {
			health -= damage;
			if (health <= 0f) {
				die ();
			}
			damageTakenTime = Time.time;
			isHurt = true;
		}
		return health;
	}

	public void die() {
		Destroy(this);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag ("Trail")) {
			takeDamage (playerController.getAttack ());

		}
	}


	public void setHealth(float health) {
		this.health = health;
	}

	public void setAttack(float attack) {
		this.attack = attack;
	}

	public void setSpeed(float speed) {
		this.speed = speed;
	}

	public void setVision(float vision) {
		this.vision = vision;
	}
}
