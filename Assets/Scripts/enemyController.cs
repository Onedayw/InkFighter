using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

	public Mover shot;
	public Transform shotSpawn;
	public float shotRate;
	private float nextShot;

	private Rigidbody2D rb2d;

	void Start() 
	{
		nextShot = 2;
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector3 (-2, -2, 0);
	}

	// Update is called once per frame
	void Update () 
	{
		if (Time.time > nextShot) {
			nextShot = Time.time + shotRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{

		if (other.CompareTag ("Trail")) {
			Destroy(gameObject);
		}

		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("Edge") && rb2d.velocity.x < 0) {
			rb2d.velocity = new Vector3 (2, 2, 0);
		} else if (other.gameObject.CompareTag ("Edge") && rb2d.velocity.x > 0) {
			rb2d.velocity = new Vector3 (-2, -2, 0);
		} 
	}

}
