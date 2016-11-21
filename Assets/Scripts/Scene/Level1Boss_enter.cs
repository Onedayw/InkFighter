using UnityEngine;
using System.Collections;

public class Level1Boss_enter : MonoBehaviour {
    public GameObject bossgate;
    public GameObject player;
    public Camera mainCamera;
    public CameraFollowing camerafollowing;
    //private bool enter = false;
    //private float gatex;
	// Use this for initialization
	void Start () {

    }
	
    void OnTriggerEnter2D(Collider2D other) {
        GameObject otherObject = other.gameObject;
        if (otherObject.CompareTag("Player")) {
            GameObject bossGate = Instantiate(bossgate) as GameObject;
            mainCamera = Camera.main;
            camerafollowing = mainCamera.GetComponent<CameraFollowing>();
        }
    }
	// Update is called once per frame
	void Update () {
	    //if (enter && player.transform.position.x >= (gatex + Screen.width/2 - camerafollowing.freeMoveOffsetX)) {
     //       camerafollowing.ChangetoBossBounds();
     //   }

    }
}
