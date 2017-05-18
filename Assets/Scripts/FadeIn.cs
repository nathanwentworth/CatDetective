using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour {

	private RawImage rawImage;
	private float alpha;
	private Color c;
	[SerializeField]
	private float fadeTime;

	private void Awake() {
		transform.root.gameObject.SetActive(true);
		rawImage = GetComponent<RawImage>();
		c = rawImage.color;
		alpha = 1.0f;
		StartCoroutine(Fade(true, 0));
	}

	public IEnumerator Fade(bool fadeDir, int scene) {
		float t = fadeTime;

		while (t > 0f) {
			alpha = (fadeDir) ? (t / fadeTime) : (-(t / fadeTime) + 1);
			c.a = alpha;
			rawImage.color = c;
			t -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		if (!fadeDir) {
			SceneManager.LoadScene(scene);
		}

		yield return null;
	}

}
