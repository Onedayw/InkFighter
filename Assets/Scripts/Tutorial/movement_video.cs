using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class movement_video : MonoBehaviour {

	public MovieTexture movie;
	public RawImage rawImage;

	// Use this for initialization
	void Start () {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Destroy (rawImage);
			Handheld.PlayFullScreenMovie ("Move.mp4");
		} else {
			GetComponent<RawImage> ().texture = movie as MovieTexture;
			movie.Play ();
			StartCoroutine(endMovie());
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator endMovie() {		
		yield return new WaitForSeconds(5.5f);
		Destroy (rawImage);
	}
}
