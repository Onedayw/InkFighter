using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialParry : MonoBehaviour {
	public GameObject player;
	public GameObject Enemy;
	public GameObject axe;
	public int count;
	public Text CountText;

	// Use this for initialization
	void Start () {
		count = 0;

		TextUpdate ();
	}

	// Update is called once per frame
	void Update () {		
	}


	void TextUpdate () {
		CountText.text = "Count: " + count.ToString();
	}
}
