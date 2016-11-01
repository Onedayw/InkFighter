using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public bool onPause = false;
    public static float pauseMenuWidth = Screen.width / 2;
    private static float pauseMenuHeight = Screen.height;
    private Rect pauseWindow = new Rect(pauseMenuWidth/2, 0, pauseMenuWidth, pauseMenuHeight);

    void OnGUI() {
        if (onPause) {
            pauseWindow = GUI.Window(0, pauseWindow, popPauseMenu, "Pause");
        }

    }
    public void onStartGame()
	{
		Debug.Log ("start");
		SceneManager.LoadScene ("Level 1");
	}

	public void reStartGame()
	{
		Debug.Log ("restart");
		SceneManager.LoadScene ("Level 1");
	}

	public void backToMenu()
	{
		SceneManager.LoadScene ("openning");
	}

	public void doTutorial() {
		SceneManager.LoadScene ("tutorial_movement");
	}
    public void pause() {
        onPause = true;
    }
    public void resumeGame() {
        onPause = false;
    }
    public void popPauseMenu(int windowID) {
        Time.timeScale = 0;
        if (GUI.Button(new Rect(pauseMenuWidth/4, pauseMenuHeight/6, pauseMenuWidth/2, pauseMenuHeight*0.05f), "Resume")) {
            Time.timeScale = 1;
            this.resumeGame();

        }
        if (GUI.Button(new Rect(pauseMenuWidth / 4, pauseMenuHeight / 3, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), "Perks")) {

        }
        if (GUI.Button(new Rect(pauseMenuWidth / 4, pauseMenuHeight / 2, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), "Skills")) {

        }
        if (GUI.Button(new Rect(pauseMenuWidth / 4, pauseMenuHeight / 1.5f, pauseMenuWidth / 2, pauseMenuHeight * 0.05f), "Main Menu")) {
            Time.timeScale = 1;
            this.resumeGame();
            this.backToMenu();
        }
    }
}