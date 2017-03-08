using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

public class ShortRangeEnemy_AxeEnemy : MonoBehaviour {
    /* pathfinding */
    //The point to move to
    public Transform target;

    private Seeker seeker;

    //The calculated path
    public Path path;

    //The AI's speed per second(use thisEnemy.speed)
    //public float speed = 2;

    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    /*short range attack*/

    public float meleeRate;// attack rate
    public float meleeRange;// attack range
    private float nextShot;// next attackable time
    private GameObject player;
    private PlayerController playerController;
    private Enemy thisEnemy;
    private Animator anim;

    private bool bIsAttack;

    /*short range movement*/
    private Rigidbody2D rb2d;
    private bool faceRight = false;

    public void Start() {
        seeker = GetComponent<Seeker>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        playerController = player.GetComponent<PlayerController>();
        thisEnemy = this.GetComponent<Enemy>();
        anim = GetComponent<Animator>();

        bIsAttack = false;
        
        nextShot = meleeRate;
    }

    public void OnPathComplete(Path p) {
        //Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }

    public void FixedUpdate() {

        if (!bIsAttack)
        { 
            //get distance between this and target(player)
            Vector3 distance = transform.position - thisEnemy.getTarget().transform.position;

            if (((Vector2)distance).magnitude < meleeRange) { //attack if in range
                if (Time.time > nextShot)
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", false);

                    StartCoroutine(setIsAttack());

                    nextShot = Time.time + meleeRate;
                }
                else
                {
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", true);
                }
            }
            else { //move if not in range, check seen target first
                if (thisEnemy.getSeenTarget()) {
                    //Start a new path to the targetPosition, return the result to the OnPathComplete function
                    seeker.StartPath(transform.position, target.position, OnPathComplete);


                    if (path == null) {
                        //We have no path to move after yet
                        return;
                    }

                    if (currentWaypoint >= path.vectorPath.Count) {
                        //Debug.Log("End Of Path Reached");
                        return;
                    }

                    if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
                        currentWaypoint++;
                        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
                        dir *= thisEnemy.speed * Time.fixedDeltaTime;
                        faceMovingDirection(dir.x);
                        this.gameObject.transform.Translate(dir, Space.World);
                        anim.SetBool("isAttacking", false);
                        anim.SetBool("isRunning", true);
                        anim.SetBool("isIdle", false);

                        return;
                    }
                }
                else if (distance.magnitude < thisEnemy.vision) { //not seen yet, check if this see the target now
                    thisEnemy.setSeenTarget();
                }

            }
        }

        return;
    }

    IEnumerator setIsAttack()
    {
        bIsAttack = true;

        yield return new WaitForSeconds(1);

        bIsAttack = false;
    }

    public void attack() {

        Vector3 distance = transform.position - thisEnemy.getTarget().transform.position;

        if (((Vector2)distance).magnitude < meleeRange)
        {
            playerController.loseHealth(thisEnemy.attack);
        }
    }

    //flip face direction
    void faceMovingDirection(float moveHorizontal) {
        if (moveHorizontal > 0 && !faceRight) {
            Flip();
        }
        else if (moveHorizontal < 0 && faceRight) {
            Flip();
        }
    }

    void Flip() {
        faceRight = !faceRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	public bool getFaceRight() {
		return faceRight;
	}
}