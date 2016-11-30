using UnityEngine;
using System.Collections;

public class camera_moveright : MonoBehaviour {
	public Camera cam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		//playerController = player.GetComponent<PlayerController>();
		if (otherObject.CompareTag("Player")) {
			//GameObject bossGate = Instantiate(bossgate) as GameObject;
			//mainCamera = Camera.main;
			//camerafollowing = mainCamera.GetC
			float halfwidth = cam.orthographicSize * ((float)Screen.width / Screen.height);
			//component<CameraFollowing>();
			Debug.Log(halfwidth); 
			Debug.Log(cam.transform.position); 
			cam.transform.Translate (new Vector3 (halfwidth, 0f), Space.World);
			Debug.Log(cam.transform.position); 
			//Destroy(this.GetComponent<BoxCollider2D>());
			//BossHealthContainer.enabled = true;
			//BossDamagedHealth.enabled = true;
			//playerController.inBossArea = true;
		}
	}
}
