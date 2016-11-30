using UnityEngine;
using System.Collections;

public class ShortRangeMove : MonoBehaviour {

	private Enemy thisEnemy;
	private Rigidbody2D rb2d;
	private ShortRangeAttack s_Attack;
	private bool faceRight = false;
	private Animator anim;
	public float speedMultiplier = 1f;
	// Use this for initialization
	void Start () {
		thisEnemy = GetComponent<Enemy> ();
		s_Attack = this.GetComponent<ShortRangeAttack>();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		Vector3 distance = transform.position - thisEnemy.getTarget ().transform.position;
		if (thisEnemy.getSeenTarget ()) {
			if (distance.magnitude > s_Attack.meleeRange) {
				move ();
				anim.SetBool ("isAttacking", false);
				anim.SetBool ("isRunning", true);
				anim.SetBool ("isIdle", false);
			}
		} else if (distance.magnitude < thisEnemy.vision) {
			thisEnemy.setSeenTarget ();
		}
	}

	public void move () {
		thisEnemy.transform.position = Vector3.MoveTowards (thisEnemy.transform.position, 
			thisEnemy.getTarget().transform.position, thisEnemy.speed * speedMultiplier * Time.deltaTime);
		faceMovingDirection (thisEnemy.getTarget().transform.position.x - thisEnemy.transform.position.x);
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
