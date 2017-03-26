using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {


	private int bossFullHealth, bossHealth;
	public Image healthBar;
	private GameObject bossObject;
	private Enemy boss;
	// Use this for initialization
	void Start () {		
		bossObject = GameObject.FindGameObjectWithTag ("Boss");
		boss = bossObject.GetComponent<Enemy> ();
		bossFullHealth = boss.fullHealth;
	}
	
	// Update is called once per frame
	void Update () {
		bossHealth = boss.health >= 0 ? boss.health : 0;
		healthBar.fillAmount = (float)bossHealth / (float)bossFullHealth;
	}
}
