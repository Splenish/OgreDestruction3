using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomizer : MonoBehaviour {

	public GameObject[] enemies;

	float numberOfEnemies;

	// Use this for initialization
	void Start () {
		Random.InitState ((int)System.DateTime.Now.Ticks);
		numberOfEnemies = Random.Range (1, 4);
		Debug.Log (numberOfEnemies);
		numberOfEnemies = Mathf.Round (numberOfEnemies);

		for (int i = 0; i < numberOfEnemies; i++) {
			enemies [i].SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
