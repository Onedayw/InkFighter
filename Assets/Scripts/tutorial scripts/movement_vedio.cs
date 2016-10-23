using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class movement_vedio : MonoBehaviour {

	public MovieTexture movie;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		movie.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
