using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ios_parry_video : MonoBehaviour {

	public RawImage rawImage;

	// Use this for initialization
	void Start () {
		Destroy (rawImage);
		Handheld.PlayFullScreenMovie ("Parry.mp4", Color.black, FullScreenMovieControlMode.Hidden);
	}

	// Update is called once per frame
	void Update () {
	}

	IEnumerator endMovie() {		
		yield return new WaitForSeconds(3.8f);
		Destroy (rawImage);
	}
}
