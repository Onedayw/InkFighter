using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

public class LongRangeEnemy_Archer : MonoBehaviour {
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

    /*long range attack*/
    public Mover mover;
    public Transform shotSpawn;

    public float shotRate;// attack rate
    public float shotRange;// attack range
    private GameObject player;
    private Enemy thisEnemy;
    private Animator anim;

    /*short range movement*/
    private Rigidbody2D rb2d;
    private bool faceRight = false;

    public bool isInBossArea = false; // check this for archers in boss area

    public void Start() {
        seeker = GetComponent<Seeker>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        thisEnemy = this.GetComponent<Enemy>();
        //anim = GetComponent<Animator>();
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
        //get distance between this and target(player)
        Vector3 distance = transform.position - thisEnemy.getTarget().transform.position;
        if (((Vector2)distance).magnitude < shotRange) { //attack if in range
            //attack();
            //anim.SetBool("isAttacking", true);
            //anim.SetBool("isRunning", false);
            //anim.SetBool("isIdle", false);
        }
        else { //move if not in range, check seen target first
            if (thisEnemy.getSeenTarget()) {
                if (isInBossArea) {// archers in bossarea do not use pathfinding(for performance optimization)
                    move();
                    return;
                }
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
					this.gameObject.transform.Translate(dir, Space.World);
                    //anim.SetBool("isAttacking", false);
                    //anim.SetBool("isRunning", true);
                    //anim.SetBool("isIdle", false);
                    return;
                }
            }
            else if (distance.magnitude < thisEnemy.vision) { //not seen yet, check if this see the target now
                thisEnemy.setSeenTarget();
            }
        }
		faceMovingDirection (thisEnemy.getTarget().transform.position.x - thisEnemy.transform.position.x);

        return;
    }

    public void attack() {
        Instantiate(mover, shotSpawn.position, shotSpawn.rotation);
    }

    public void move() {
        thisEnemy.transform.position = Vector3.MoveTowards(thisEnemy.transform.position,
            thisEnemy.getTarget().transform.position, thisEnemy.speed * Time.deltaTime);
        //anim.SetBool("isAttacking", false);
        //anim.SetBool("isRunning", true);
        //anim.SetBool("isIdle", false);
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
}