using UnityEngine;
using System.Collections;
/*
 * enable camera following
 * camera will not follow unless the player has crossed the boundary
 * attach this to main camera
 */
public class CameraFollowing : MonoBehaviour {
	//boundary: central area size of 0.6 * 0.6
	public float freeMoveOffsetX = (float)(Screen.width * 0.2);
	public float freeMoveOffsetY = (float)(Screen.height * 0.2);
	Camera mycam;
	public Vector2 margin;
	public GameObject player;  
	public BoxCollider2D bounds;
    public BoxCollider2D bossBounds;
    private Vector3 _min, _max;
	private float playerPositionX, playerPositionY, cameraPositionX, cameraPositionY, cameraPositionZ;

	void Start () {
		_min = bounds.bounds.min;
		_max = bounds.bounds.max;
		mycam = GetComponent<Camera> ();
		freeMoveOffsetX = SceneToWorldSize(freeMoveOffsetX, Camera.main, this.transform.position.z);
		freeMoveOffsetY = SceneToWorldSize(freeMoveOffsetY, Camera.main, this.transform.position.z);

	}

	void LateUpdate () {
		playerPositionX = player.transform.position.x;
		playerPositionY = player.transform.position.y;
		cameraPositionX = this.transform.position.x;
		cameraPositionY = this.transform.position.y;

		if (playerPositionX > cameraPositionX + freeMoveOffsetX) {
			cameraPositionX += playerPositionX - cameraPositionX - freeMoveOffsetX;
		}
		else if(playerPositionX < cameraPositionX - freeMoveOffsetX) {
			cameraPositionX -= cameraPositionX - freeMoveOffsetX - playerPositionX;
		}

		if (playerPositionY > cameraPositionY + freeMoveOffsetY) {
			cameraPositionY += playerPositionY - cameraPositionY - freeMoveOffsetY;
		}
		else if (playerPositionY < cameraPositionY - freeMoveOffsetY) {
			cameraPositionY -= cameraPositionY - freeMoveOffsetY - playerPositionY;
		}

		var halfwidth = mycam.orthographicSize * ((float)Screen.width / Screen.height);
		cameraPositionX = Mathf.Clamp (cameraPositionX, _min.x + halfwidth, _max.x - halfwidth);
		cameraPositionY = Mathf.Clamp (cameraPositionY, _min.y + mycam.orthographicSize, _max.y - mycam.orthographicSize);
		transform.position = new Vector3 (cameraPositionX, cameraPositionY, transform.position.z);
	}

	public float SceneToWorldSize(float size, Camera ca, float Worldz) {
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
    public void ChangetoBossBounds() {
        _min = bossBounds.bounds.min;
        _max = bossBounds.bounds.max;
        //Debug.Log("using boss bound");
        //bossBounds.GetComponent<Collider2D>().enabled;
    }
}
