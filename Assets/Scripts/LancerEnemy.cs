using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerEnemy : MonoBehaviour {
    public float attackRate = 3f;
    public float dashRange = 6f;
    public GameObject target;
    public bool isDashing = false;
    public float dashSpeed = 7f;
    private Enemy thisEnemy;
    private Vector3 direction, dashTo, currentPosition;
    private float startTime, journeyLength;
    private PlayerController playerController;
    private float nextAttack;



    void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        thisEnemy = this.GetComponent<Enemy>();
        nextAttack = attackRate;
        playerController = target.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (true) {
             float distance = ((Vector2)(transform.position - target.transform.position)).magnitude;

            if (!isDashing) {
                if (distance > dashRange * 0.8f  || Time.time < nextAttack) {
                    move();
                }
                else {
                    currentPosition = this.transform.position;
                    startTime = Time.time;

                    direction = (target.transform.position - currentPosition).normalized;
                    dashTo = currentPosition + direction * dashRange;
                    journeyLength = Vector3.Distance(currentPosition, dashTo);
                    isDashing = true;
                    
                }

                //this.transform.Translate(direction * 3f);
            }//!isdashing
            else {
                Dash();
            }
        }//seentarget

    }//update

    void move() {
        thisEnemy.transform.position = Vector3.MoveTowards(thisEnemy.transform.position,
        target.transform.position, thisEnemy.speed * Time.deltaTime);
        //faceMovingDirection(thisEnemy.getTarget().transform.position.x - thisEnemy.transform.position.x);
    }

    void Dash() {
        nextAttack = Time.time + attackRate;
        float distcovered = (Time.time - startTime) * dashSpeed;
        float fracjourney = distcovered / journeyLength;
        //transform.position = Vector3.Lerp(currentPosition, dashTo, fracjourney);
        thisEnemy.transform.position = Vector3.MoveTowards(thisEnemy.transform.position,
        dashTo, thisEnemy.speed * Time.deltaTime*3);
        if (fracjourney >= 1) {
            isDashing = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player") && isDashing) {
            Debug.Log("123");
            playerController.loseHealth(thisEnemy.attack);
        }
    }
}
