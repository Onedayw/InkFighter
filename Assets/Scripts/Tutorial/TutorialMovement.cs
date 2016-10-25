using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialMovement : MonoBehaviour {
	public GameObject player;
	private Vector3 origin;

	// Use this for initialization
	void Start () {
		origin = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position != origin) {
			StartCoroutine(toNextScene());
		}
	}

	IEnumerator toNextScene() {		
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene ("tutorial_kill");
	}
}
