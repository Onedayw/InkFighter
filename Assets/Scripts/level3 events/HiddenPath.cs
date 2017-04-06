using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPath : MonoBehaviour {
    public PlayerController playerController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("0");
            playerController = other.GetComponent<PlayerController>();
            Debug.Log(playerController.isDashing);
            if (playerController.isDashing) {
                Destroy(this.gameObject);
            }
        }

    }
}
