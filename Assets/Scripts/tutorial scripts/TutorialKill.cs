using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialKill : MonoBehaviour {
	public GameObject player;
	public GameObject Enemy;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Enemy == null) {
			SceneManager.LoadScene ("tutorial_parry");
		}
	}
}
