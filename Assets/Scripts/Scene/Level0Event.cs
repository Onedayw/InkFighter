using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level0Event : MonoBehaviour {

	private int inkNumber;
	private int axeNumber;
	private int targetNumber;
	private bool skillOn;
	public GameObject Gate1;
	public GameObject Gate2;

	void Start () {		
		inkNumber = 5;
		axeNumber = 5;
		skillOn = false;
		Handheld.PlayFullScreenMovie ("Move.mp4", Color.black, FullScreenMovieControlMode.Hidden);

	}
	

	void Update () {
		if (skillOn == true) {			
			targetNumber = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			if (targetNumber == 0) {
				SceneManager.LoadScene ("Level 1");
			}
		}
	}

	public void setInkNumber() {
		inkNumber--;
		if (inkNumber == 0) {
			Destroy (Gate1);
		}
	}

	public void setAxeNumber() {
		axeNumber--;
		if (axeNumber == 0) {
			Destroy (Gate2);
		}
	}

	public void setSkillOn() {
		skillOn = true;
	}
}
