using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private GameObject target;
    private PlayerController playerController;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        playerController = target.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerController.setHasCheckPos(true);
            playerController.setCheckPos(transform.position);
        }
    }
}
