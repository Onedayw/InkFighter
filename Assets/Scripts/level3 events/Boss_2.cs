using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2 : MonoBehaviour {
    private Enemy thisEnemy;
    private Vector2 inNormal = Vector2.left;
    private Vector2 movement = new Vector2(1, 1);
    private PlayerController playercontroller;
    private int axeDamage = 30, lanceDamage = 30;
    private float lanceWide = 5;
    public GameObject player;
    public Mover arrow;
    public TrackingMover trackingInk;
    public Transform[] shotSpawn = new Transform[3];
    public float speed = 3f;

    //public float shotRate = 1.5f;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playercontroller = player.GetComponent<PlayerController>();
        for(int i = 0; i <3; i++) {
            shotSpawn[i] = this.transform;
        }
        thisEnemy = this.GetComponent<Enemy>();
        speed = (float)thisEnemy.speed;
    }
	
	// Update is called once per frame
	void Update () {
        speed = 3f + 1 / (thisEnemy.health / (float)thisEnemy.fullHealth);
        float r = Random.value;
        Vector2 distance = transform.position - player.transform.position;
        if(distance.magnitude > 5) {
            if(r < 0.01) {
                arrowAttack();
            }
        }
        else {
            if (r < 0.005) {//axe attack
                axeAttack();
            }
            else if (r < 0.01) {//lance attack
                lanceAttack();
            }
        }
	}
    void FixedUpdate() {

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(1,0,1);
        transform.Translate(movement * 6f * Time.deltaTime, Space.World);
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("BossArea")){
            Instantiate(trackingInk, transform.position,transform.rotation);
            Instantiate(trackingInk, transform.position - new Vector3(1,0,0), transform.rotation);
            string name = other.gameObject.name;
            switch (name) {
                case "right":
                    inNormal = Vector2.right;
                    movement = Vector2.Reflect(movement, inNormal);
                    break;
                case "left":
                    inNormal = Vector2.left;
                    movement = Vector2.Reflect(movement, inNormal);
                    break;
                case "up":
                    inNormal = Vector2.down;
                    movement = Vector2.Reflect(movement, inNormal);
                    break;
                case "down":
                    inNormal = Vector2.down;
                    movement = Vector2.Reflect(movement, inNormal);
                    break;

            }
        }
    }

   private void arrowAttack() {
        for (int i = 0; i < 3; i++) {
            Instantiate(arrow, shotSpawn[i].position, shotSpawn[i].rotation);
        }
    }
   private void axeAttack() {
        //relevant animation
        playercontroller.loseHealth(axeDamage);
   }
   private void lanceAttack() {
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        float dif = Mathf.Abs(x) - Mathf.Abs(y);
        if (x >= 0 || dif >= 0) {//right
            if(Mathf.Abs(y) < (lanceWide / 2)) {
                playercontroller.loseHealth(lanceDamage);
            }
        }
        if (x < 0 || dif >= 0) {//left
            if (Mathf.Abs(y) < (lanceWide / 2)) {
                playercontroller.loseHealth(lanceDamage);
            }
        }
        if (y >= 0 || dif < 0) {//up
            if (Mathf.Abs(x) < (lanceWide / 2)) {
                playercontroller.loseHealth(lanceDamage);
            }
        }
        if (y < 0 || dif >= 0) {//down
            if (Mathf.Abs(x) < (lanceWide / 2)) {
                playercontroller.loseHealth(lanceDamage);
            }
        }
        //relevant animation
        playercontroller.loseHealth(axeDamage);
    }
}
