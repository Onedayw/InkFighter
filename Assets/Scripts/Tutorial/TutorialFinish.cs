using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialFinish : MonoBehaviour {

	void Start () {
		StartCoroutine(toNextScene());
	}

	IEnumerator toNextScene() {		
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene ("openning");
	}
}
