using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialParry : MonoBehaviour {
	public GameObject player;
	public GameObject Enemy;
	public static int parryAXE = 0;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (parryAXE >= 3) {
			toNextScene ();	
		}
	}

	void toNextScene() {
		SceneManager.LoadScene ("openning");
	}
}
