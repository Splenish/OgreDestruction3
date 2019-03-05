using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameReset : MonoBehaviour {


    public GameObject gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			ResetToStartMenu ();
		}
	}


	public void ResetToStartMenu() {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "BattleScene") {
            Time.timeScale = 1f;
            gm.GetComponent<GameManager>().ActivateUnits();
        }

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("WorldEnemy");
		GameObject[] pickups = GameObject.FindGameObjectsWithTag ("Pickup");


		for (int i = 0; i < enemies.Length; i++) {
			Destroy (enemies[i]);
		}

		for (int i = 0; i < pickups.Length; i++) {
			Destroy (pickups[i]);
		}

		Destroy (GameObject.Find ("A*"));
		Destroy (GameObject.Find ("EnemySpawner"));
		Destroy (GameObject.Find ("Player"));
		Destroy (GameObject.Find ("PickupManager"));

		Destroy (GameObject.Find ("GameManager"));
		SceneManager.LoadScene ("mainmenu");
	}
}

