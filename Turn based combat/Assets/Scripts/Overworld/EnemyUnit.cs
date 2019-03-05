using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit {

	public int aggroRange;

	public Vector3 target;

	GameObject player;

	GameObject aggroTrigger;

	public GameObject grid;


	int enemyUnits;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		player = GameObject.Find ("Player");
		gm = GameObject.Find ("GameManager");
		enemyUnits = gm.GetComponent<GameManager> ().enemyUnits.Count;
		remainingMovement = moveSpeed;
		//currentUnit = this.gameObject;
		anim = this.gameObject.GetComponent<Animator> ();
		grid = GameObject.Find ("A*");
		float combatTriggerSize = grid.GetComponent<GameGrid> ().nodeRadius * aggroRange * 2 - 3;
		aggroTrigger = gameObject.transform.Find ("CombatTrigger").gameObject;
		//aggroTrigger.transform.localScale = new Vector3 (combatTriggerSize, 1, combatTriggerSize);
		moving = false;
		//Debug.Log ("enemy unit start");
		//Debug.Log ("start");
	}

	// Update is called once per frame
	void LateUpdate () {
		target = player.transform.position;
		aggroTrigger.transform.localRotation = Quaternion.Inverse (transform.rotation);
		GameManager.GameState gs = gm.GetComponent<GameManager> ().CurrentGameState;
		//Debug.Log ("gs enemyUnitis: " + gs);
		Debug.Log (this.gameObject + "remaining movement" + remainingMovement);
		if (gm.GetComponent<GameManager> ().currentUnit.gameObject != null) {
			if (gs == GameManager.GameState.enemyTurn && gm.GetComponent<GameManager> ().currentUnit.gameObject == this.gameObject) {
				if (checkIfInAggrorange ()) {
					//Debug.Log ("if in aggrorange");
					moving = true;
				
					FinishEnemyTurn ();
					MoveUnit ();
				} else {
					gm.GetComponent<GameManager> ().i++;
//					Debug.Log ("i++");
					//remainingMovement = moveSpeed;
					//Debug.Log ("GM:n i: " + gm.GetComponent<GameManager> ().i);
					if (gm.GetComponent<GameManager> ().i > enemyUnits - 1) {
						Debug.Log ("PLayerin vuorolle");
						gm.GetComponent<GameManager> ().CurrentGameState = GameManager.GameState.myTurn;
						player.GetComponent<Player> ().StartTurn ();
						remainingMovement = moveSpeed;
					}	
				}
			}
		}
		Debug.Log ("remaining movement player turnil = " + remainingMovement);
	}

	void FixedUpdate() {
		Debug.Log ("paska");
	}

	bool checkIfInAggrorange() {
		if (currentPath != null) {
			if (currentPath.Count <= aggroRange) {
				//Debug.Log ("Aggrorange");
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	void FinishEnemyTurn() {
		if (Vector3.Distance (transform.position, currentPath [0].worldPosition) < .02f) {
			if (remainingMovement <= 0) {
				gm.GetComponent<GameManager> ().i++;
				if (gm.GetComponent<GameManager> ().i > enemyUnits - 1) {
					//Debug.Log ("koklet");

					gm.GetComponent<GameManager> ().CurrentGameState = GameManager.GameState.myTurn;
					player.GetComponent<Player> ().StartTurn ();
					//remainingMovement = moveSpeed;
					//moving = false;
				}
			}
		}
	}

	public void StartEnemyTurn() {
		Debug.Log ("start enemyturn");
		//Debug.Log ("minka enemyunit script: " + this.gameObject);

		remainingMovement = moveSpeed;
		Debug.Log ("Remaining movement = " + remainingMovement);
		Debug.Log ("moveSpeed = " + moveSpeed);
	}


	public void PullTrigger(Collider c) {
		if (c.gameObject.tag == "Player") {
			gm.GetComponent<GameManager> ().StartCombat (this.gameObject);
		}
	}

	/*
	public void MoveToNextTile() {

		Debug.Log ("enemy to next tile");

		if (currentPath == null) {			
			return;
		}

		//	Debug.Log ("remaingingMovement--");
		remainingMovement--;

		if (remainingMovement <= 0) {
			moving = false;
			remainingMovement = moveSpeed;
			return;
		}

		transform.position = currentPath [0].worldPosition;

		//remove the old current tile
		currentPath.RemoveAt(0);
		if (currentPath.Count == 0) {
			moving = false;
			currentPath = null;
			remainingMovement = moveSpeed;
		}
	}*/
}