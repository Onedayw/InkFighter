using UnityEngine;
using System.Collections;

public class AxeEnemy : Enemy {
	
	// Use this for initialization
	void Start () {
		setAttackBehavior (new LongRangeAttack ());
		setMoveBehavior (new LongRangeMove ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
