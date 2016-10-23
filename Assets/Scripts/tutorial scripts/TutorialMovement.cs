using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialMovement : MonoBehaviour {
	public GameObject player;
	private Vector3 origin;
	bool flag;
	private float firstTime;

	// Use this for initialization
	void Start () {
		origin = player.transform.position;
		flag = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position != origin) {
			if (flag) {
				firstTime = Time.time;
				flag = false;
			}
			toNextScene ();
		}
	}

	void toNextScene() {		
		if (Time.time - firstTime >= 3f) {
			SceneManager.LoadScene ("tutorial_kill");
		}
	}
}
