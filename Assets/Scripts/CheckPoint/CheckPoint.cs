using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour {

    private GameObject Player;
    private PlayerController playerController;
    private List<int> enemyList;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<PlayerController>();
	}

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerController.setCheckPos(transform.position);

            SaveLoad.s_SaveLoad.level = SceneManager.GetActiveScene().buildIndex;
            for (int i = 0; i < EnemyLoadManager.s_enemyManager.flag.Length; i++)
                if (EnemyLoadManager.s_enemyManager.flag[i] == 1)
                    enemyList.Add(1);
                else
                    enemyList.Add(0);
            SaveLoad.s_SaveLoad.enemyList = enemyList;
            SaveLoad.s_SaveLoad.playerPosition = transform.position;

            SaveLoad.s_SaveLoad.Save();
        }
    }
}
