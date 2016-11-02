using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class movement_video : MonoBehaviour {

	public MovieTexture movie;
	public RawImage rawImage;

	// Use this for initialization
	void Start () {
	//	GetComponent<RawImage> ().texture = movie as MovieTexture;
	//	movie.Play ();
	//	StartCoroutine(endMovie());
		Handheld.PlayFullScreenMovie("Move.mp4");
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator endMovie() {		
		yield return new WaitForSeconds(5.5f);
		Destroy (rawImage);
	}
}
