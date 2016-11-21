using UnityEngine;
using System.Collections;

public class LongRangeMove : MonoBehaviour {

	private Enemy thisEnemy;
	private Rigidbody2D rb2d;
    private LongRangeAttack l_Attack;
	private bool faceRight = false;

	// Use this for initialization
	void Start () {
		thisEnemy = GetComponent<Enemy> ();
        l_Attack = this.GetComponent<LongRangeAttack>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 distance = transform.position - thisEnemy.getTarget ().transform.position;
		if (thisEnemy.getSeenTarget ()) {
			if (distance.magnitude > l_Attack.shotRange) {
				move ();
			}
		} else if (distance.magnitude < thisEnemy.vision) {
			thisEnemy.setSeenTarget ();
		}
		faceMovingDirection (thisEnemy.getTarget().transform.position.x - thisEnemy.transform.position.x);
	}

	public void move () {
		thisEnemy.transform.position = Vector3.MoveTowards (thisEnemy.transform.position, 
			thisEnemy.getTarget().transform.position, thisEnemy.speed * Time.deltaTime);
	}

	void faceMovingDirection(float moveHorizontal) 
	{
		if (moveHorizontal > 0 && !faceRight) {
			Flip ();
		}
		else if (moveHorizontal < 0 && faceRight) {
			Flip ();
		}
	}

	void Flip ()
	{
		faceRight = !faceRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
