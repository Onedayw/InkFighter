using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tutorail : MonoBehaviour {
	
	bool mouseOver = false;
	public Button tutorailButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {
		mouseOver = true;
		tutorailButton.image.enabled = false;
	}
}
