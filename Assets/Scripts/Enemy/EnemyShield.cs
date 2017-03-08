using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour {

	public GameObject owner;
	private ShortRangeEnemy_AxeEnemy script;

	// Use this for initialization
	void Start () {
		script = owner.GetComponent<ShortRangeEnemy_AxeEnemy> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (script.getFaceRight () == true) {
			this.transform.position = owner.transform.position + new Vector3 (2, 0, 0);
			Debug.Log ("right");
		} else {
			this.transform.position = owner.transform.position + new Vector3 (-2, 0, 0);
			Debug.Log ("left");
		}
	}


}
