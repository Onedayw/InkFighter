using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private float health, attack, speed, vision;
	private AttackBehavior ab;
	private MoveBehavior mb;

	private EnemyManager enemyManager;
	private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
	private GameObject target;                           //Transform to attempt to move toward each turn.

	// Use this for initialization
	void Start () {
		health = 100f;
		attack = 100f;
		speed = 100f;
		vision = 100f;
		target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Usc this for taking damage
	public float takeDamage(float damage) {
		health -= damage;
		if (health <= 0f) {
			die ();
		}
		return health;
	}

	public void die() {
		Destroy(this);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag ("Trail")) {


		}
	}

	public void setAttackBehavior(AttackBehavior ab) {
		this.ab = ab;
	}

	public void setMoveBehavior(MoveBehavior mb) {
		this.mb = mb;
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
