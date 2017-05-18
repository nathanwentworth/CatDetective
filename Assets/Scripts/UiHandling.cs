using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiHandling : MonoBehaviour {

	[SerializeField]
	private GameObject mainMenuCanvas;
	[SerializeField]
	private GameObject pauseCanvas;
	[SerializeField]
	private GameObject inventoryCanvas;

	[SerializeField]
	private Button mainMenuButton;
	[SerializeField]
	private Button quitButton;
	[SerializeField]
	private Button playButton;

	public bool paused;
	public bool invActive;
	
	private FadeIn fade;

	private void Awake() {
		GameObject fadeCanvas = null;
		if ((fadeCanvas = GameObject.Find("FadeCanvas")) != null) {
			fade = fadeCanvas.GetComponentInChildren<FadeIn>();
		}
	}

	private void Start () {
		playButton.onClick.AddListener(PlayOnClick);
		quitButton.onClick.AddListener(QuitOnClick);
		mainMenuButton.onClick.AddListener(MenuOnClick);

		if (SceneManager.GetActiveScene().name != "MainMenu"){
			mainMenuCanvas.SetActive(false);
		}
		pauseCanvas.SetActive (false);
		inventoryCanvas.SetActive (false);
	}
	
	private void Update(){
		if (Input.GetKeyDown ("escape")) {
			if (invActive == true) {
				inventoryCanvas.SetActive (false);
				Debug.Log ("INVENTORY DOWN");
				invActive = false;
			} else if (paused == true) {
				pauseCanvas.SetActive (false);
				Debug.Log ("UNPAUSED");
				paused = false;
			} else {
				pauseCanvas.SetActive (true);
				Debug.Log ("PAUSED");
				paused = true;
			}
		}
		if (Input.GetKeyDown (KeyCode.Tab) && paused == false) {
			if (invActive == true) {
				inventoryCanvas.SetActive (false);
				Debug.Log ("INVENTORY DOWN");
				invActive = false;
			} else {
				inventoryCanvas.SetActive (true);
				Debug.Log ("INVENTORY UP");
				invActive = true;
			}
		}

	}

	void PlayOnClick(){
		if (fade != null) {
			StartCoroutine(fade.Fade(false, 1));
		} else {
			Debug.LogWarning("No fade UI in the scene!");
			SceneManager.LoadScene (1);
		}
	}

	void QuitOnClick(){
		Application.Quit();
	}

	void MenuOnClick(){
		SceneManager.LoadScene(0);
	}


}
