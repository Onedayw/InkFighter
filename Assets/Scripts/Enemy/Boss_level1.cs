using UnityEngine;
using System.Collections;

public class Boss_level1 : MonoBehaviour {
	public int dashingMultiplier;
	public float meleeRate;
	private float nextShot;
	public GameObject target;
	private Vector3 dashDirection;
	private PlayerController playerController;
	private Enemy thisEnemy;
	public float meleeRange;
	public bool isdash = false;
	public bool isstun = false;
    public Rigidbody2D rb;
    float stunTime = 0.0f;

	private bool faceRight = false;

	private Animator anim;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
		isdash = false;
		nextShot = 2;
		target = GameObject.FindGameObjectWithTag ("Player");
		playerController = target.GetComponent<PlayerController> ();
		thisEnemy = this.GetComponent<Enemy> ();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		Vector3 distance = transform.position - target.transform.position;
		if (thisEnemy.getSeenTarget ()) {
            if (isdash) {
                dash();
				anim.SetBool ("bossIdle", false);
				anim.SetBool ("isRunning", false);
				anim.SetBool ("isAttacking", false);
				anim.SetBool ("isDashing", true);
            }
            else if (isstun) {
				anim.SetBool ("bossIdle", true);
				anim.SetBool ("isRunning", false);
				anim.SetBool ("isAttacking", false);
				anim.SetBool ("isDashing", false);
                if(Time.time>stunTime + 2f) {
                    isstun = false;
                }
            }
            else {
                if (((Vector2)distance).magnitude < meleeRange) {
                    if (!isdash && !isstun) {
                        float r = Random.value;

                        if (r < 0.999) {
                            //Debug.Log("attacking");
                            attack();
							anim.SetBool ("bossIdle", false);
							anim.SetBool ("isRunning", false);
							anim.SetBool ("isAttacking", true);
							anim.SetBool ("isDashing", false);
                        }
                        else {
                            //Debug.Log("dashing");
                            isdash = true;
                            dashDirection = (target.transform.position - thisEnemy.transform.position).normalized;
                        }
                    }
                }
                if (((Vector2)distance).magnitude > meleeRange) {
                    if (!isdash && !isstun) {
                        float r = Random.value;
                        if (r < 0.989) {
							move();
							anim.SetBool ("bossIdle", false);
							anim.SetBool ("isRunning", true);
							anim.SetBool ("isAttacking", false);
							anim.SetBool ("isDashing", false);
                        }
                        else {
                            //Debug.Log("dashing");
                            isdash = true;
                            dashDirection = (target.transform.position - thisEnemy.transform.position).normalized;
                        }
                    }
                    //Debug.Log ("im moving");
                }
            }
		} else if (((Vector2)distance).magnitude < thisEnemy.vision) {
			thisEnemy.setSeenTarget ();
		}
	}

	public void move () {
        //Debug.Log("moving");
        thisEnemy.transform.position = Vector3.MoveTowards (thisEnemy.transform.position, 
			target.transform.position, thisEnemy.speed * Time.deltaTime);
		faceMovingDirection (thisEnemy.getTarget().transform.position.x - thisEnemy.transform.position.x);
	}



	public void attack () {
		if (Time.time > nextShot) {
			nextShot = Time.time + meleeRate;
			playerController.loseHealth (thisEnemy.attack);
		}
	}

	public void dash(){
        
		thisEnemy.transform.position += dashDirection * thisEnemy.speed * dashingMultiplier * Time.deltaTime;
    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("BossArea")){
            //Debug.Log("hit");
            isdash = false;
            isstun = true;
            stunTime = Time.time;
		}
        if (other.gameObject.CompareTag("Player") && isdash) {
            attack();
        }
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

	public bool getFaceRight() {
		return faceRight;
	}
}
