using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level1Boss_enter : MonoBehaviour {
    public GameObject bossgate;
    public GameObject player;
    public Camera mainCamera;
    public CameraFollowing camerafollowing;
	public Image BossHealthContainer;
	public Image BossFullHealth;
    private PlayerController playerController;
    //private bool enter = false;
    //private float gatex;
	// Use this for initialization
	void Start () {		
		player = GameObject.FindGameObjectWithTag ("Player");
    }
	
    void OnTriggerEnter2D(Collider2D other) {
        GameObject otherObject = other.gameObject;
        playerController = player.GetComponent<PlayerController>();
        if (otherObject.CompareTag("Player")) {
            GameObject bossGate = Instantiate(bossgate) as GameObject;
            mainCamera = Camera.main;
            camerafollowing = mainCamera.GetComponent<CameraFollowing>();
            //Debug.Log("boss!");
			Destroy(this.GetComponent<BoxCollider2D>());
			BossHealthContainer.enabled = true;
			BossFullHealth.enabled = true;
            playerController.inBossArea = true;
        }
    }
	// Update is called once per frame
	void Update () {
	    //if (enter && player.transform.position.x >= (gatex + Screen.width/2 - camerafollowing.freeMoveOffsetX)) {
     //       camerafollowing.ChangetoBossBounds();
     //   }

    }
}
