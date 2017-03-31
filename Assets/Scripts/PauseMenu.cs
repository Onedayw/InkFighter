using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Transform PauseUI;
    public MusicManager MusicManager;


	// Use this for initialization
	void Start () {
        PauseUI.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Pause()
    {
        if (PauseUI.gameObject.activeInHierarchy == false)
        {
            PauseUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseUI.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void MainMenu()
    {
		PauseUI.gameObject.SetActive(false);
		Time.timeScale = 1;
        SceneManager.LoadScene("openning");
    }

    public void MusicChanged()
    {
        float vol = GameObject.Find("MusicSlider").GetComponent<UnityEngine.UI.Slider>().value;

        MusicManager.SetVolume(vol);
    }

    public void Quit()
    {
        Application.Quit();
    }
}