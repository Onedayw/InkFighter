using UnityEngine;
using System.Collections;

public class ink_spot : MonoBehaviour {

	private Level0Event level0;
	public GameObject EventSystem;
	// Use this for initialization
	void Start () {
		level0 = EventSystem.GetComponent<Level0Event>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) 
	{		
		if (other.CompareTag ("Player")) {
			Destroy(gameObject);
			level0.setInkNumber ();
		}
	}

}
