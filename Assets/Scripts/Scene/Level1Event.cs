using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level1Event : MonoBehaviour {

	private int enemyNumber;
	private int bossNumber;

	// Use this for initialization
	void Start () {
		//enemyNumber = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		bossNumber = GameObject.FindGameObjectsWithTag ("Boss").Length;
	}
	
	// Update is called once per frame
	void Update () {
		//enemyNumber = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		bossNumber = GameObject.FindGameObjectsWithTag ("Boss").Length;
		if (bossNumber == 0) {
			SceneManager.LoadScene ("openning");
		}
	}
}
