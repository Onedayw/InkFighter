using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoadManager : MonoBehaviour {
    public static EnemyLoadManager s_enemyManager;

    public GameObject[] Enemy;
    public int[] flag;      //0 = 死亡；1 = 存活；2 = 上一个check point后打死，但还没存

    private void Awake()
    {
        if (s_enemyManager != null)
            GameObject.Destroy(s_enemyManager);
        else
            s_enemyManager = this;

        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        for(int i = 0; i < flag.Length; i++)
        {
            if (flag[i] == 0)
                Destroy(Enemy[i]);
        }
	}
}
