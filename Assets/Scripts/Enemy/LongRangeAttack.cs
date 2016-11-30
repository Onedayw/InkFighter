using UnityEngine;
using System.Collections;

public class LongRangeAttack : MonoBehaviour {
	
	public Mover mover;
	public Transform shotSpawn;
	public float shotRate;
	private float nextShot;
	private GameObject target;
    public float shotRange;

	// Use this for initialization
	void Start () {
		nextShot = 2;
		target = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		if ((target.transform.position - this.transform.position).magnitude < shotRange && GetComponent<Enemy>().getSeenTarget()) {
			attack ();
		}
	}

	public void attack () {
		if (Time.time > nextShot) {
			nextShot = Time.time + shotRate;
			Instantiate (mover, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag ("Trail")) {
            //Destroy(gameObject);
            //Debug.Log("1");
		}
	}
}
