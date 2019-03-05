using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {



	public static PickupManager Instance;


	//GameObject[] healthPotions;

	public GameObject pickUp;

	// Use this for initialization
	void Start () {

		if (Instance == null) {
            Debug.Log("healthpickup spawn");
            DontDestroyOnLoad(gameObject);
            Instance = this;
            GameObject[] locationObjects = GameObject.FindGameObjectsWithTag ("PickUpLocation");

			Quaternion rotation =  Quaternion.identity;
            rotation = Quaternion.Euler(30, 0, 0);

			for (int i = 0; i < locationObjects.Length; i++) {
				Instantiate (pickUp, locationObjects[i].transform.position, rotation);
			}

			//healthPotions = GameObject.FindGameObjectsWithTag ("PickUp");
		} else if(Instance != this) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
