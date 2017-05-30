using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	private const float hurtTime = 1f;


	//public int health, attack, speed, vision, money;
	public bool isInBossArea;
	public int fullHealth, attack, speed, vision, money;
	public int health;

	private float damageTakenTime;
	private bool isHurt, seenTarget;
	public Image healthBar;
	// private EnemyManager enemyManager;
	private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
	private GameObject target;                           //Transform to attempt to move toward each turn.
	private PlayerController playerController;
	private float alphaLevel;
	private Rigidbody2D rb2d;

	private static float enemyDeadTime = 0.4f;

	// Use this for initialization
	void Start () {
		health = fullHealth;
		isHurt = false;
		target = GameObject.FindGameObjectWithTag ("Player");
		playerController = target.GetComponent<PlayerController> ();
		animator = GetComponent<Animator> ();
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
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
				//updatecolor ();
				updateHealth ();
				healthBar.enabled = true;
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
		GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, alphaLevel);
	}

	// let enemy healthbar move left out to the mask
	void updateHealth() {		
		healthBar.fillAmount = (float)health / (float)fullHealth;
	}

	public void die() {
		animator.SetBool ("isDead", true);
        for (int i = 0; i < EnemyLoadManager.s_enemyManager.Enemy.Length; i++)
        {
            if (EnemyLoadManager.s_enemyManager.flag[i] == 2 || EnemyLoadManager.s_enemyManager.flag[i] == 0)
                continue;
            if (this.name == EnemyLoadManager.s_enemyManager.Enemy[i].name)
            {
                EnemyLoadManager.s_enemyManager.flag[i] = 2;
                break;
            }
        }

        Destroy(this.gameObject, enemyDeadTime);
		playerController.addMoney (money);
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.CompareTag ("Trail") && playerController.isInRange(rb2d)) {
			takeDamage (playerController.getAttack ());
			//Debug.Log(playerController.getAttack());
		}
		if (other.CompareTag ("PlayerMover")) {
			//takeDamage (15);
			//Debug.Log(playerController.getAttack());
		}
	}
}
