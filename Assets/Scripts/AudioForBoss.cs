using UnityEngine;
using System.Collections;

public class AudioForBoss : MonoBehaviour {

	//public AudioClip shootSound;
	public AudioSource source;
	public Camera cam;
	public AudioSource MainBGM;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		Vector3 screenPoint = cam.WorldToViewportPoint(this.transform.position);
		//Debug.Log (screenPoint.x+" "+screenPoint.y+" "+screenPoint.z);
		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
		//bool onScreen =renderer.isVisible;
		if (onScreen&&!source.isPlaying) {
			//Debug.Log ("IM IN");
			MainBGM.Stop();
			source.Play();
		}
	}
}
