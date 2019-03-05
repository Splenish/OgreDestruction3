using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandPlacedEnemy : MonoBehaviour {

	public GameObject enemy;

	public Quaternion rotation;

	public static SpawnHandPlacedEnemy Instance;

	public Transform[] spawnLocations;

	// Use this for initialization
	void Awake () {




		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
	
			rotation = Quaternion.identity;

			rotation.eulerAngles = new Vector3 (0, -90, 0);

			GameObject[] locationObjects = GameObject.FindGameObjectsWithTag ("SpawnLocation");

			for (int i = 0; i < locationObjects.Length; i++) {
				Instantiate (enemy, locationObjects[i].transform.position, rotation);
			}
			//Instantiate (enemy, this.transform.position, rotation);

		} else if(Instance != this) {
			Destroy(gameObject);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
