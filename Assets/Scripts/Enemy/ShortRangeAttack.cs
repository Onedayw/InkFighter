using UnityEngine;
using System.Collections;

public class ShortRangeAttack : MonoBehaviour {

	public float vision;
	public float meleeRate;
	private float nextShot;
	private GameObject target;
	private PlayerController playerController;
	private Enemy thisEnemy;
	public float meleeRange;

	// Use this for initialization
	void Start () {
		nextShot = 2;
		target = GameObject.FindGameObjectWithTag ("Player");
		playerController = target.GetComponent<PlayerController> ();
		thisEnemy = this.GetComponent<Enemy> ();
	}

	// Update is called once per frame
	void Update () {
		if ((target.transform.position - this.transform.position).magnitude < meleeRange) {
			attack ();
		}
	}

	public void attack () {
		if (Time.time > nextShot) {
			nextShot = Time.time + meleeRate;
			playerController.loseHealth (thisEnemy.attack);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag ("Trail")) {
			//Destroy(gameObject);
			Debug.Log("1");
		}
	}
}
