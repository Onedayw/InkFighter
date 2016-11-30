using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillArea : MonoBehaviour {

	public GameObject player;
	private PlayerController playerController;
	private SpriteRenderer feibiao;
	public GameObject EventSystem;
	private Level0Event level0;
	public GameObject target1;
	public GameObject target2;
	public GameObject target3;
	public GameObject target4;
	public GameObject target5;
	public GameObject target6;
	public GameObject target7;
	public GameObject target8;
	public Image circle;
	public Camera cam;

	void Start () {
		level0 = EventSystem.GetComponent<Level0Event>();
		feibiao = gameObject.GetComponent<SpriteRenderer> ();
		playerController = player.GetComponent<PlayerController> ();
		playerController.setHasCircleSkill (false);
		StartCoroutine(FBflashing());
	}
	
	// Update is called once per frame
	void Update () {		
	}

	void OnTriggerEnter2D(Collider2D other) 
	{		
		if (other.CompareTag ("Player")) {			
			playerController.setHasCircleSkill (true);
			playerController.speed = 0;
			circle.enabled = true;
			target1.SetActive (true);
			target2.SetActive (true);
			target3.SetActive (true);
			target4.SetActive (true);
			target5.SetActive (true);
			target6.SetActive (true);
			target7.SetActive (true);
			target8.SetActive (true);
			level0.setSkillOn ();
			//Time.timeScale = 0.6f;
			Destroy(gameObject);
			//Debug.Log (cam.transform.position);
			//Debug.Log (other.transform.position);
			cam.transform.position = new Vector3(other.transform.position.x,other.gameObject.transform.position.y,cam.transform.position.z);
			//Debug.Log (cam.transform.position);
			cam.orthographicSize = 4.3f;
			other.gameObject.transform.position = this.transform.position;
		}
	}

	IEnumerator FBflashing() {
		while (true) {
			feibiao.enabled = true;
			yield return new WaitForSeconds(1);
			feibiao.enabled = false;
			yield return new WaitForSeconds(0.1f);
		}
	}

}
