using UnityEngine;
using System.Collections;

public class Level0Event : MonoBehaviour {

	private int inkNumber;
	private int axeNumber;
	public GameObject Gate1;
	public GameObject Gate2;
	public GameObject Gate3;

	void Start () {		
		inkNumber = 5;
		axeNumber = 5;
		Handheld.PlayFullScreenMovie ("Move.mp4", Color.black, FullScreenMovieControlMode.Hidden);
	}
	

	void Update () {

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
}
