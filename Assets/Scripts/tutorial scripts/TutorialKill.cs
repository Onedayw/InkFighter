using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialKill : MonoBehaviour {
	public GameObject player;
	public GameObject Enemy;
	public GameObject Enemy1;
	public GameObject Enemy2;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Enemy == null && Enemy1 == null && Enemy2 == null) {
			SceneManager.LoadScene ("tutorial_parry");
		}
	}
}
