using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static float timeInDay = 19f;
	private Vector3 lastKnownPos;	

	private void Awake () {
		DontDestroyOnLoad(gameObject);
	}
		

}
