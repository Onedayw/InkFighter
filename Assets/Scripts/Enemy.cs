using UnityEngine;
using System.Collections;

namespace Completed {
	public class Enemy : MonoBehaviour {
		public float health, attack, speed, vision;

		private EnemyManager enemyManager;
		private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
		private Transform target;                           //Transform to attempt to move toward each turn.


		// Use this for initialization
		void Start () {
			enemyManager = GetComponent<EnemyManager> ();
			enemyManager.addEnemyToList (this.gameObject);
			animator = GetComponent<Animator> ();
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		
		// Update is called once per frame
		void Update () {
		
		}

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
	}
}
