using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToWorld : MonoBehaviour {

	GameObject gm;
	public GameObject goBackButton;
	Button theButton;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager");
		//goBackButton = GameObject.Find ("BackButton").GetComponent<Button>();
		theButton = goBackButton.GetComponent<Button>();
		theButton.onClick.AddListener (GoBack);
	}


	public void GoBack() {
		SceneManager.LoadScene ("world");
		gm.GetComponent<GameManager> ().ActivateUnits ();	
	}
}


