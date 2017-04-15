using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour {

    public static SaveLoad s_SaveLoad;

    public int level;
    public List<int> enemyList = new List<int>();
    public Vector3 playerPosition = new Vector3();

    private savedate data;

    private void Awake()
    {
        if (s_SaveLoad != null)
            GameObject.Destroy(s_SaveLoad);
        else
            s_SaveLoad = this;

        DontDestroyOnLoad(this);

        Load();
    }

    public void Save()
    {
        if (!Directory.Exists (Application.dataPath + "/saves"))
            Directory.CreateDirectory (Application.dataPath + "/saves");

        FileStream fstream = File.Create(Application.dataPath + "/saves/SaveData.ink");

        BinaryFormatter bf = new BinaryFormatter();

        data.enemyList.Clear();

        data.level = level;
        foreach(int i in enemyList)
        {
            data.enemyList.Add(i);
        }
        data.playerPosition = Vector3ToSerVector3(playerPosition);

        bf.Serialize(fstream, data);
        fstream.Close();
    }

    public void saveLevel(int m_level)
    {
        level = m_level;
    }

    public void saveEnemyList(int[] m_enemyList)
    {
        for(int i = 0; i < m_enemyList.Length; i++)
        {
            enemyList.Add(m_enemyList[i]);
        }
    }

    public void savePlayerPosition(Vector3 m_playerPosition)
    {
        playerPosition = m_playerPosition;
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/saves/SaveData.ink"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fstream = File.Open(Application.dataPath + "/saves/SaveData.ink", FileMode.Open);

            data.enemyList.Clear();

            data = (savedate)bf.Deserialize(fstream);

            level = data.level;
            foreach (int i in data.enemyList)
            {
                enemyList.Add(i);
            }
            playerPosition = SerVector3ToVector3(data.playerPosition);

            fstream.Close();
        }
    }

    public int loadLevel()
    {
        return level;
    }

    public List<int> loadEnemyList()
    {
        return enemyList;
    }

    public Vector3 loadPlayerPosition()
    {
        return playerPosition;
    }

    private static SerVector3 Vector3ToSerVector3(Vector3 V3)
    {
        SerVector3 SV3 = new SerVector3();

        SV3.x = V3.x;
        SV3.y = V3.y;
        SV3.z = V3.z;

        return SV3;
    }

    private static Vector3 SerVector3ToVector3(SerVector3 SV3)
    {
        Vector3 V3 = new Vector3();

        V3.x = SV3.x;
        V3.y = SV3.y;
        V3.z = SV3.z;

        return V3;
    }
}

[System.Serializable]
public class savedate
{
    public int level;
    public List<int> enemyList = new List<int>();
    public SerVector3 playerPosition;
}

[System.Serializable]
public class SerVector3
{
    public float x;
    public float y;
    public float z;
}