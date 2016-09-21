using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;                
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private int enenyNumer = 0;

	void Start () 
	{
		InvokeRepeating ("Spawn", 0, spawnTime);
	}

	void Update () {
		enenyNumer = GameObject.FindGameObjectsWithTag ("Enemy").Length;
	}

	void Spawn ()
	{

		if (enenyNumer < 5) {
			// Find a random index between zero and one less than the number of spawn points.
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);

			// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		}

	}
}
