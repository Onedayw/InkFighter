using UnityEngine;
using System.Collections;

public class fingercontrol : MonoBehaviour {

	public GameObject Square;
	//private int i=1;
	//private LinkedList<Transform> list;
	// Use this for initialization
	void Start () {
		//Instantiate(Square, new Vector3(i,i, 0), Quaternion.identity);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Input.touchCount > 0) {
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).phase == TouchPhase.Began) {
					Vector2 mouse = Input.GetTouch (i).position;
					if (mouse.x > 220 || mouse.y > 220) {
						Vector3 objPos = Camera.main.ScreenToWorldPoint (new Vector3(mouse.x, mouse.y, 2));
						Instantiate(Square);
						//Debug.Log ("inputN:"+i);
						//obj.SendMessage ("insi", i);
						//AsteroidScript p = obj.GetComponent<AsteroidScript> ();
						//p.inputNum = i;
					}
				}
			}		
		}
		//i=i*4;
		//Instantiate(Square, new Vector3(i,i, 0), Quaternion.identity);

	}

}
