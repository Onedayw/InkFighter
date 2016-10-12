using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tutorail : MonoBehaviour {
	
	public Button tutorailButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {
		tutorailButton.image.enabled = false;
	}
}
