using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightToggle : MonoBehaviour {

	[SerializeField]
	private Light flickeringLight;
	public bool lightOn;
	[SerializeField]
	private float lightChance;
	public bool lamp;

	private void Start () {
		lightOn = true;
	}
	
	private void Update () {
		if (lamp){
			lightOn = (Random.value < 0.9);
			flickeringLight.gameObject.SetActive(lightOn);
		} else {
			lightOn = (Random.value < lightChance);
			flickeringLight.intensity -= Time.deltaTime * 30;
			if (lightOn) {
				flickeringLight.intensity = 30f;
			}

		}

	}
}
