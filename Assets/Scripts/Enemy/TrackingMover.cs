using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingMover : MonoBehaviour {
    private GameObject target;
    private PlayerController playerController;
    private Rigidbody2D rb2d;
    public float speed = 2;
    public int attack = 10;
    private static float moverExplodeDelay = 0.4f;
    private Animator anim;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerController = target.GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position,
target.transform.position, this.speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        GameObject otherObject = other.gameObject;
        if (this.CompareTag("EnemyMover")) {
            if (otherObject.CompareTag("Player") || otherObject.CompareTag("Edge")) {
                rb2d.velocity = new Vector3(0, 0, 0);
                if (anim != null) anim.SetBool("exploded", true);
                Destroy(gameObject, moverExplodeDelay);
            }
            if (otherObject.CompareTag("Player")) {
                makeDamage(attack);
            }
            if (otherObject.CompareTag("Trail")) {
                if (anim != null) anim.SetBool("exploded", true);
                Destroy(gameObject, moverExplodeDelay);
            }
        }
        else if (this.CompareTag("PlayerMover")) {
            if (otherObject.CompareTag("Enemy") || otherObject.CompareTag("Edge")) {
                Destroy(gameObject, moverExplodeDelay);
            }
            if (otherObject.CompareTag("Enemy")) {
                Enemy enemy = otherObject.GetComponent<Enemy>();
                enemy.takeDamage(attack);
            }
        }
    }

    void makeDamage(int damage) {
        playerController.loseHealth(damage);
    }
}
