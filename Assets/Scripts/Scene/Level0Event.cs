using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level0Event : MonoBehaviour {

	private int inkNumber;
	private int axeNumber;
	private int targetNumber;
	private bool skillOn;
	public GameObject Gate1;
	public GameObject Gate2;
	public Image move;
	public Image kill;
	public Image circle;

	void Start () {		
		inkNumber = 5;
		axeNumber = 5;
		skillOn = false;
		Handheld.PlayFullScreenMovie ("Move.mp4", Color.black, FullScreenMovieControlMode.Hidden);
		move.enabled = true;
	}
	

	void Update () {
		if (skillOn == true) {			
			targetNumber = GameObject.FindGameObjectsWithTag ("Enemy").Length;
			if (targetNumber == 0) {
				circle.enabled = false;
				SceneManager.LoadScene ("Level 1");
			}
		}
	}

	public void setInkNumber() {
		inkNumber--;
		if (inkNumber == 0) {
			Destroy (Gate1);
			move.enabled = false;
		}
	}

	public void setAxeNumber() {
		axeNumber--;
		if (axeNumber == 0) {
			Destroy (Gate2);
			kill.enabled = false;
		}
	}

	public void setSkillOn() {
		skillOn = true;
	}
}
