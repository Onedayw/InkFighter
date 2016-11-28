using UnityEngine;
using System.Collections;

public class ShortRangeAttack : MonoBehaviour {

	public float meleeRate;
	private float nextShot;
	private GameObject target;
	private PlayerController playerController;
	private Enemy thisEnemy;
	public float meleeRange;
	private Animator anim;

	// Use this for initialization
	void Start () {
		nextShot = 2;
		target = GameObject.FindGameObjectWithTag ("Player");
		playerController = target.GetComponent<PlayerController> ();
		thisEnemy = this.GetComponent<Enemy> ();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if ((target.transform.position - this.transform.position).magnitude < meleeRange) {
			attack ();
			anim.SetBool ("isAttacking", true);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isIdle", false);
		}
	}

	public void attack () {
		if (Time.time > nextShot) {
			nextShot = Time.time + meleeRate;
			playerController.loseHealth (thisEnemy.attack);
		}
	}
}
