using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public void onStartGame()
	{
		Debug.Log ("start");
		SceneManager.LoadScene ("main_g");
	}

	public void reStartGame()
	{
		SceneManager.LoadScene ("main_g");
	}

	public void backToMenu()
	{
		SceneManager.LoadScene ("openning");
	}
}