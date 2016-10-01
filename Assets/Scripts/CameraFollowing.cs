using UnityEngine;
using System.Collections;
/*
 * enable camera following
 * camera will not follow unless the player has crossed the boundary
 * attach this to main camera
 */
public class CameraFollowing : MonoBehaviour {
	//boundary: central area size of 0.6 * 0.6
	private float freeMoveOffsetX = (float)(Screen.width * 0.3);
	private float freeMoveOffsetY = (float)(Screen.height * 0.3);
	public GameObject player;  
	private float playerPositionX, playerPositionY, cameraPositionX, cameraPositionY, cameraPositionZ;

	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");
		freeMoveOffsetX = SceneToWorldSize(freeMoveOffsetX, Camera.main, this.transform.position.z);
		freeMoveOffsetY = SceneToWorldSize(freeMoveOffsetY, Camera.main, this.transform.position.z);
	}

	void LateUpdate () {
		playerPositionX = player.transform.position.x;
		playerPositionY = player.transform.position.y;
		cameraPositionX = this.transform.position.x;
		cameraPositionY = this.transform.position.y;
		if (playerPositionX > cameraPositionX + freeMoveOffsetX)
		{
			transform.position += new Vector3((playerPositionX - cameraPositionX - freeMoveOffsetX), 0);
		}
		else if(playerPositionX < cameraPositionX - freeMoveOffsetX)
		{
			transform.position -= new Vector3((cameraPositionX - freeMoveOffsetX - playerPositionX), 0);
		}

		if (playerPositionY > cameraPositionY + freeMoveOffsetY)
		{
			transform.position += new Vector3(0,(playerPositionY - cameraPositionY - freeMoveOffsetY));
		}
		else if (playerPositionY < cameraPositionY - freeMoveOffsetY)
		{
			transform.position -= new Vector3(0,(cameraPositionY - freeMoveOffsetY - playerPositionY));
		}
	}

	public float SceneToWorldSize(float size, Camera ca, float Worldz)
	{
		if (ca.orthographic)
		{
			float height = Screen.height / 2;
			float px = (ca.orthographicSize / height);
			//Debug.Log("1");
			return px * size;
		}
		else
		{
			float halfFOV = (ca.fieldOfView * 0.5f);
			halfFOV *= Mathf.Deg2Rad;
			float height = Screen.height / 2;
			float px = height / Mathf.Tan(halfFOV);
			Worldz = Worldz - ca.transform.position.z;
			return (Worldz / px) * size;
		}
	}
}
