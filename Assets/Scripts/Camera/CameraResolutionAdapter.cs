using UnityEngine;
using System.Collections;
/* 
 * Auto adjusts camera to ensure camera's width & height >= designed width & height
 * attach this to main camera
 */
public class CameraResolutionAdapter : MonoBehaviour {
	//screen size: 1334 * 750 (iPhone 6s, 16:9)
	public float screenHeight = 7.5f;
	public float screenWidth = 13.34f;

	void Start () {
		float orthographicSize = this.GetComponent<Camera>().orthographicSize;
		float aspectRatio = Screen.width * 1.0f / Screen.height;
		float cameraWidth = orthographicSize * 2 * aspectRatio;
		if(cameraWidth < screenWidth)
		{
			orthographicSize = screenWidth / aspectRatio / 2;
			this.GetComponent<Camera>().orthographicSize = orthographicSize;
		}


	}


	void Update () {

	}
}
