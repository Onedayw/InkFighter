using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialParry : MonoBehaviour {
	public GameObject player;
	public GameObject Enemy;
	public static int parryAXE;


	// Use this for initialization
	void Start () {
		parryAXE = 0;
	}

	// Update is called once per frame
	void Update () {
		if (parryAXE >= 3) {
			toNextScene ();	
		}
	}

	void toNextScene() {
		SceneManager.LoadScene ("tutorial_finish");
	}
}
