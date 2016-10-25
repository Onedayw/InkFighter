using UnityEngine;
using System.Collections;

public class LongRangeAttack : AttackBehavior {
	public Mover mover;
	public Transform shotSpawn;
	public float shotRate;
	private float nextShot;


	// Use this for initialization
	void Start () {
		nextShot = 2;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void attack () {

	}
}
