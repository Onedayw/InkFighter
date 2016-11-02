using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;                
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private int enenyNumer = 0;
	private ArrayList enemies;

	void Start () 
	{
		enemies = new ArrayList ();
	//	InvokeRepeating ("Spawn", 0, spawnTime);
	}

	void Update () {
		enenyNumer = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if(enenyNumer==0) SceneManager.LoadScene("openning");
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

	public void addEnemyToList(GameObject e) {
		enemies.Add(e);
	}
}
