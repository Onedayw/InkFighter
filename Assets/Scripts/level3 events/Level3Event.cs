using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level3Event : MonoBehaviour {

    private int enemyNumber;
    private int bossNumber;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject target5;
    // Use this for initialization
    void Start() {
        //enemyNumber = GameObject.FindGameObjectsWithTag ("Enemy").Length;
        bossNumber = GameObject.FindGameObjectsWithTag("Boss").Length;
    }

    // Update is called once per frame
    void Update() {
        //enemyNumber = GameObject.FindGameObjectsWithTag ("Enemy").Length;
        bossNumber = GameObject.FindGameObjectsWithTag("Boss").Length;
        if (bossNumber == 0) {
            SceneManager.LoadScene("openning");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            target1.SetActive(true);
            target2.SetActive(true);
            target3.SetActive(true);
            target4.SetActive(true);
            target5.SetActive(true);

            //Time.timeScale = 0.6f;
            Destroy(gameObject);
        }
    }
}

