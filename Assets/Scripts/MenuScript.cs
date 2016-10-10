using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public void onStartGame()
	{
		Debug.Log ("start");
		SceneManager.LoadScene ("main_g");
	}

}