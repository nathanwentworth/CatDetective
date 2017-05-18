using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Fungus;

public class PlayerController : MonoBehaviour {

	public Transform moveTarget;
	private NavMeshAgent playerAgent;	
	public doorScript doorStuff;
	[SerializeField]
	private Flowchart flowchart;

	private FadeIn fade;

	private void Awake() {
		GameObject fadeCanvas = null;
		if ((fadeCanvas = GameObject.Find("FadeCanvas")) != null) {
			fadeCanvas.SetActive(true);
			fade = fadeCanvas.GetComponentInChildren<FadeIn>();
		}
	}

	private void Start () {
		playerAgent = GetComponent<NavMeshAgent>();

	}

	private void FixedUpdate(){
		//if player object is not close to the target already
		//return the object clicked
		//move the player there
		//if player object is close, interact with it
		if (!flowchart.HasExecutingBlocks()) {
			if (Input.GetMouseButtonDown(0)){
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast(ray, out hit)){
					if (hit.rigidbody != null){
						if (hit.collider.tag == "moveEnabled") {
							moveTarget.transform.position = hit.point;
						} else if (hit.collider.tag == "clue") {
							if (Vector3.Distance (gameObject.transform.position, hit.transform.position) < 4f) {
								string name = hit.collider.name;
								Debug.Log ("you examined the " + name);
								switch (name) {
									case "Briefcase":
										flowchart.ExecuteBlock("Finding-the-Suitcase");
										break;
									default:
										Debug.LogWarning("You didn't set up this object yet!");
										break;
								}
							} else {
								moveTarget.transform.position = hit.transform.position;
							}
						} else if (hit.collider.tag == "door"){
							if (Vector3.Distance (gameObject.transform.position, hit.transform.position) < 3f) {
								doorStuff = hit.collider.gameObject.GetComponent<doorScript>();

								if (fade != null) {
									StartCoroutine(fade.Fade(false, doorStuff.sceneNumberToLoad));
								} else {
									Debug.LogWarning("No fade UI in the scene!");
									SceneManager.LoadScene (doorStuff.sceneNumberToLoad);
								}
								

								
							} else {
								moveTarget.transform.position = hit.transform.position;
							}
						}
					}
				}

			}
				
			playerAgent.destination = moveTarget.position;			
		}
	}

}
