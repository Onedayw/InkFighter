using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour {
	public GameObject Paper;
	public GameObject LeftScroll1;
	public GameObject LeftScroll2;
	public GameObject LeftScroll3;
	public GameObject LeftScroll4;
	public GameObject LeftScroll5;
	public GameObject LeftScroll6;
	public GameObject LeftScroll7;
	private GameObject[] leftScroll;
	private float paperStartPosition;
	private float paperPosition;
	private float maxPaperPosition;

	void Start () {
		leftScroll = new GameObject[] {LeftScroll1, LeftScroll2, LeftScroll3, LeftScroll4, LeftScroll5, LeftScroll6, LeftScroll7};
		paperStartPosition = Paper.transform.position.x;
		maxPaperPosition = (paperStartPosition + 32.38f) * 1.8f;
		LeftScroll1.SetActive (true);
		LeftScroll2.SetActive (false);
		LeftScroll3.SetActive (false);
		LeftScroll4.SetActive (false);
		LeftScroll5.SetActive (false);
		LeftScroll6.SetActive (false);
		LeftScroll7.SetActive (false);
	}
	

	void Update () {		
		paperPosition = (paperStartPosition - Paper.transform.position.x) * 1.8f;
		if (paperPosition < 0) {
			Paper.transform.position = new Vector3 (paperStartPosition, 0.05f, 0);
			return;
		}
		if (paperPosition > maxPaperPosition) {
			Paper.transform.position = new Vector3 (-32.38f, 0.05f, 0);
			return;
		}
		Debug.Log (paperPosition);
		int remain = (int)paperPosition % 7;
		leftScroll[remain].SetActive (true);
		for (int i = 0; i < 7; i++) {
			if (i != remain) {
				leftScroll[i].SetActive (false);
			}
		}
	}
}
