using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	public GameObject player;
    public Transform PauseUI;
	private PlayerController playerController;
	public GameObject[] finger; 
	private AsteroidScript trail;
    public bool onPause = false;
	public bool onPerks = false;
	public bool onConfirm = false;
	public bool noEnoughMoney = false;
	public bool purchasedHealthBoost = false;
	public bool purchasedSpeedBoost = false;
	public bool purchasedAttackBoost = false;
	public bool purchasedRangeBoost = false;
	private string perksName;

    public static float pauseMenuWidth = Screen.width / 2;
    private static float pauseMenuHeight = Screen.height;
	public static float perksMenuWidth = Screen.width / 4 * 3;
	private static float perksMenuHeight = Screen.height / 4 * 3;
	private static float perksMenuStart = Screen.width / 9;
	private static float perksItemStart = Screen.width / 8;
	private static float perksItemHeight = Screen.height / 6;
    private Rect pauseWindow = new Rect(pauseMenuWidth / 2, 0, pauseMenuWidth, pauseMenuHeight);
	private Rect perksWindow = new Rect(perksMenuStart, 0, perksMenuWidth, perksMenuHeight);
	private Rect perksConfirm = new Rect(perksMenuStart * 2.75f, perksItemHeight, perksMenuWidth / 2, perksMenuHeight / 2);
	private Rect noEnoughWindow = new Rect(perksMenuStart * 2.75f, perksItemHeight, perksMenuWidth / 2, perksMenuHeight / 2);
	private static string healthBoost = "Heal Rate Boost - 50 Ink";
	private static string speedBoost = "Speed Boost - 50 Ink";
	private static string attackBoost = "Attack Boost - 50 Ink";
	private static string rangeBoost = "Range Boost - 50 Ink";
	private static string backToPause = "Back To Pause Menu";
	private static string yesText = "Yes";
	private static string noText = "No";
	private GUIStyle guiStyle;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();
		finger = GameObject.FindGameObjectsWithTag ("Ink");
		trail = finger[0].GetComponent<AsteroidScript> ();
		perksName = "";
		guiStyle = new GUIStyle ();
		guiStyle.fontSize = 1;

        PauseUI.gameObject.SetActive(false);
    }
    void OnGUI () {
		if (onPause) {
            PauseUI.gameObject.SetActive(true);
            Time.timeScale = 0;
			//pauseWindow = GUI.Window (0, pauseWindow, popPauseMenu, "Pause");
		} 
		if (onPerks) {
			perksWindow = GUI.Window(1, perksWindow, perksMenu, "Perks");
		}
		if (onConfirm) {
			perksConfirm = GUI.Window (2, perksConfirm, popConfirm, "");
		}
		if (noEnoughMoney) {
			noEnoughWindow = GUI.Window (3, noEnoughWindow, notEnough, "");
		}
    }
    public void onStartGame()
	{
		//Debug.Log ("start");
		SceneManager.LoadScene ("Level 1");
	}

	public void reStartGame()
	{
		//Debug.Log ("restart");
		SceneManager.LoadScene ("Level 1");
	}

	public void backToMenu()
	{
		SceneManager.LoadScene ("openning");
	}

	public void doTutorial() {
		SceneManager.LoadScene ("Level 0");
	}
    public void pause() {
        onPause = true;
		onPerks = false;
    }
    public void resumeGame() {
        onPause = false;
		onPerks = false;
    }
	public void toPerks() {
		onPerks = true;
	}

    public void popPauseMenu(int windowID) {
        Time.timeScale = 0;
		if (GUI.Button(new Rect(pauseMenuWidth / 4, pauseMenuHeight / 6, pauseMenuWidth / 2, pauseMenuHeight*0.05f), "Resume", guiStyle)) {
            Time.timeScale = 1;
            this.resumeGame();

            PauseUI.gameObject.SetActive(false);
        }
        if (GUI.Button(new Rect(pauseMenuWidth / 4, pauseMenuHeight / 3, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), "Perks")) {
			onPause = false;
			this.toPerks ();
        }
        if (GUI.Button(new Rect(pauseMenuWidth / 4, pauseMenuHeight / 2, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), "Skills")) {

        }
        if (GUI.Button(new Rect(pauseMenuWidth / 4, pauseMenuHeight / 1.5f, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), "Main Menu")) {
            Time.timeScale = 1;
            this.resumeGame();
            this.backToMenu();
        }
    }

	public void perksMenu(int windowID) {
		Time.timeScale = 0;
        //if (GUI.Button(new Rect(perksItemStart, perksItemHeight, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), healthBoost)) {
        //    onConfirm = true;
        //    perksName = healthBoost;
        //    purchasedHealthBoost = true;
        //}
        //if (GUI.Button(new Rect(perksItemStart * 3, perksItemHeight, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), speedBoost)) {
        //    onConfirm = true;
        //    perksName = speedBoost;
        //    purchasedSpeedBoost = true;
        //}
        //if (GUI.Button(new Rect(perksItemStart, perksItemHeight * 2, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), attackBoost)) {
        //    onConfirm = true;
        //    perksName = attackBoost;
        //    purchasedAttackBoost = true;
        //}
        //if (GUI.Button(new Rect(perksItemStart * 3, perksItemHeight * 2, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), rangeBoost)) {
        //    onConfirm = true;
        //    perksName = rangeBoost;
        //    purchasedRangeBoost = true;
        //}
        if (GUI.Button(new Rect(perksItemStart*2, perksItemHeight, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), healthBoost)) {
            onConfirm = true;
            perksName = healthBoost;
            purchasedHealthBoost = true;
        }
        if (GUI.Button(new Rect(perksItemStart * 2, perksItemHeight*1.9f, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), speedBoost)) {
            onConfirm = true;
            perksName = speedBoost;
            purchasedSpeedBoost = true;
        }
        if (GUI.Button(new Rect(perksItemStart*2, perksItemHeight * 2.8f, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), attackBoost)) {
            onConfirm = true;
            perksName = attackBoost;
            purchasedAttackBoost = true;
        }


        if (GUI.Button(new Rect(pauseMenuWidth / 2, pauseMenuHeight / 1.5f, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), backToPause)) {
			pause ();
		}
	}

	public void popConfirm (int windowID) {
		Time.timeScale = 0;
		GUI.Label (new Rect(10, perksItemHeight / 2, perksMenuWidth / 2, perksMenuHeight / 2), 
			"Are you sure you want to purchase Perks : " + perksName + "?");
		if (GUI.Button(new Rect(pauseMenuWidth / 5.5f, pauseMenuHeight / 4.5f, pauseMenuWidth / 5.5f, pauseMenuHeight * 0.05f), yesText)) {
			onConfirm = false;
			if (playerController.getMoney () < 50) {
				noEnoughMoney = true;
			} else {
				playerController.deductMoney (50);
				if (purchasedHealthBoost) {
					playerController.boostSelfHealingRate ();
					purchasedHealthBoost = false;
				} else if (purchasedSpeedBoost) {
					playerController.boostSpeed ();
					purchasedSpeedBoost = false;
				} else if (purchasedAttackBoost) {
					playerController.boostAttack ();
					purchasedAttackBoost = false;
				} else if (purchasedRangeBoost) {
					playerController.boostInkRange ();
					purchasedRangeBoost = false;
				}
			}
		}
		if (GUI.Button(new Rect(pauseMenuWidth / 2.75f, pauseMenuHeight / 4.5f, pauseMenuWidth / 5.5f, pauseMenuHeight * 0.05f), noText)) {
			onConfirm = false;
		}
	}

	public void notEnough (int windowID) {
		Time.timeScale = 0;
		GUI.Label (new Rect(10, perksItemHeight / 2, perksMenuWidth / 2, perksMenuHeight / 2), 
			"Sorry but you don't have enough ink money to purchase this Perk");
		if (GUI.Button(new Rect(pauseMenuWidth / 4.0f, pauseMenuHeight / 4.5f, pauseMenuWidth / 4.0f, pauseMenuHeight * 0.05f), "OK")) {
			noEnoughMoney = false;
		}
	}
}