using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

	public int moveSpeed;
	public int remainingMovement;

	public bool moving = false;

	public List<Node> currentPath = null;

	Vector3 tilePosToMoveTo, direction;

	Quaternion rotation;

	//public GameObject currentUnit;

	public GameObject gm;

	public Animator anim;

	public float rotationSpeed;

	public Text moveText;

	public float animationMoveSpeed;

	// Use this for initialization
	void Start () {



		remainingMovement = moveSpeed;
		//currentUnit = this.gameObject;
		anim = this.gameObject.GetComponentInChildren<Animator> ();
		gm = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	void FixedUpdate () {
		MoveUnit ();		
	}


	public void MoveToNextTile() {

		if (currentPath == null) {			
			return;
		}

		GameManager.GameState gs = gm.GetComponent<GameManager> ().CurrentGameState;

	//	Debug.Log ("remaingingMovement--");

		if(remainingMovement > 0)
			remainingMovement--;

		if(gs == GameManager.GameState.myTurn && moveText != null)
			moveText.text = remainingMovement.ToString() + "/" + moveSpeed.ToString();

		if (remainingMovement <= 0) {
			moving = false;
			return;
		}

		transform.position = currentPath [0].worldPosition;

		//remove the old current tile
		currentPath.RemoveAt(0);
		if (currentPath.Count == 0) {
			moving = false;
			currentPath = null;
			//remainingMovement = moveSpeed;
		}
	}


	/*
	public void NextTurn() {
		GameManager.GameState gs = gm.GetComponent<GameManager> ().CurrentGameState;
		switch (gs) {
		case GameManager.GameState.myTurn:
			Debug.Log ("Playerin vuoro loppuu");
			gm.GetComponent<GameManager> ().CurrentGameState = GameManager.GameState.enemyTurn;
			return;
		case GameManager.GameState.enemyTurn:
			Debug.Log ("Enemyn vuoro loppuu");
			gm.GetComponent<GameManager> ().CurrentGameState = GameManager.GameState.myTurn;
			remainingMovement = moveSpeed;
			firstMove = true;
			moveText.text = remainingMovement.ToString () + "/" + moveSpeed.ToString ();
			break;
		}
	}
	*/

	public void MoveUnit() {
		if (currentPath != null && moving ) {

			if (Vector3.Distance (transform.position, currentPath [0].worldPosition) < .2f) {
				MoveToNextTile ();
			}

			if (currentPath != null) {
				tilePosToMoveTo = currentPath [0].worldPosition;

				GameManager.GameState gs = gm.GetComponent<GameManager> ().CurrentGameState;
				if (gs == GameManager.GameState.myTurn) {
					tilePosToMoveTo.y = 3.05f;
				}
			}
			direction = tilePosToMoveTo - transform.position;
			direction = Vector3.RotateTowards (transform.forward, direction, rotationSpeed*Time.deltaTime, 0.0f);

			transform.rotation = Quaternion.LookRotation (direction);

			transform.position = Vector3.MoveTowards(transform.position,tilePosToMoveTo,animationMoveSpeed);

		}

		if (moving)
			anim.SetBool ("isMoving", true);
		if (!moving)
			anim.SetBool ("isMoving", false);
	}



}