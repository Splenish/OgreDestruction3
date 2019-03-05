using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public GameObject currentUnit;


	GameObject battleEnemy;

	bool enemyTurnStart = true;

	bool playerTurnStart = true;

	public int i = 0;

	//public GameObject[] enemyUnits;

	public List<GameObject> enemyUnits;

	public List<GameObject> battleEnemies;

	GameObject player;

	public GameObject enemy;

	public List<Vector3> spawnPoints;

	public Node[,] grid;

	public GameObject gridObj;

	public Quaternion rotation;

	public int enemiesToSpawn;

	public int areaWhereEnemiesDontSpawn;


	public GameObject heroPrefab1, heroPrefab2;

	public static GameManager Instance;
	public enum GameState
	{
		myTurn,
		enemyTurn
	}
	private GameState currentGameState;
	public GameState CurrentGameState
	{
		get { return currentGameState; }
		set { currentGameState = value; }
	}
	// Use this for initialization
	void Awake()
	{

		if (currentGameState == GameState.myTurn)
			currentUnit = player;


//		Debug.Log (currentUnit);
//		Debug.Log (currentGameState);

		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;

			currentUnit = GameObject.Find ("Player");
			currentGameState = GameState.myTurn;
			player = GameObject.Find ("Player");


			rotation = Quaternion.identity;

			rotation.eulerAngles = new Vector3 (0, -90, 0);

			//grid = GetComponent<GameGrid> ().grid;
			grid = gridObj.GetComponent<GameGrid>().grid;

//			Debug.Log (grid);


			SpawnEnemies ();


			//enemyUnits = GameObject.FindGameObjectsWithTag ("Enemy");

			GameObject[] tempUnits = GameObject.FindGameObjectsWithTag ("WorldEnemy");

			foreach (GameObject unit in tempUnits) {
				enemyUnits.Add (unit);
			}

	//		Debug.Log (enemyUnits.Count);

			PlayerPrefs.DeleteAll ();
//			Debug.Log ("delete prefs");

			PlayerPrefs.SetFloat ("Hero1BaseHP", heroPrefab1.GetComponent<HeroStateMachine> ().hero.baseHP);
			PlayerPrefs.SetFloat ("Hero2BaseHP", heroPrefab2.GetComponent<HeroStateMachine> ().hero.baseHP);

	//		Debug.Log ("hero1basehp = " + PlayerPrefs.GetFloat ("Hero1BaseHP"));

		} else if(Instance != this) {
			Destroy(gameObject);
		}


	}

	// Update is called once per frame
	void Update()
	{

		switch (currentGameState)
		{
		case GameState.myTurn:
			currentUnit = player;

			if (playerTurnStart) {
				currentUnit.GetComponent<Player> ().StartTurn ();
				playerTurnStart = false;
				enemyTurnStart = true;
			}
			break;

		case GameState.enemyTurn:
			if(i < enemyUnits.Count)
				currentUnit = enemyUnits [i];
			
			if (currentUnit == null) {
				Debug.Log ("skidaadle");
				//currentGameState = GameState.myTurn;
				//i++;
			}
			Debug.Log ("current unit gm: " + currentUnit);
			if (enemyTurnStart) {
				Debug.Log ("if enemyturnstart");
				currentUnit.GetComponent<EnemyUnit> ().StartEnemyTurn ();
				enemyTurnStart = false;
				playerTurnStart = true;
			}
			break;
		}
	}

	public void StartCombat(GameObject triggeredEnemy) {

		//battleEnemy = triggeredEnemy;


		battleEnemies.Clear ();

		battleEnemies.Add (triggeredEnemy);

		Debug.Log ("vomat");
		player.SetActive (false);

		for (int j = 0; j < enemyUnits.Count; j++) {
			enemyUnits [j].SetActive (false);
		}

		SceneManager.LoadScene ("BattleScene");
	}

	void SpawnEnemies() {
		for (int k = 0; k < grid.GetLength (0); k++) {
			for (int j = 0; j < grid.GetLength (1); j++) {
				if(grid[k,j].walkable && !(k < areaWhereEnemiesDontSpawn))
					spawnPoints.Add (grid [k, j].worldPosition); 
			}
		}


		for (int j = 0; j < enemiesToSpawn; j++) {
			int spawnPointIndex = Random.Range (0, spawnPoints.Count);
			Instantiate (enemy, spawnPoints [spawnPointIndex], rotation);
			spawnPoints.RemoveAt (spawnPointIndex);
		}

//		Debug.Log ("skeltas spawned");

	}
		
	public void ActivateUnits() {
		//Destroy (enemyUnits [i]);

		Debug.Log (battleEnemies.Count);

		for (int j = 0; j < battleEnemies.Count; j++) {
			
			GameObject enemyToDestroy = battleEnemies [j];
            DestroyImmediate(enemyToDestroy.gameObject);
            enemyUnits.Remove(battleEnemies[j]);
			Debug.Log (enemyToDestroy);
			
		}
		//enemyUnits.Remove(battleEnemy);

		//Destroy (battleEnemy);

		//enemyUnits.RemoveAt (i);

		//Debug.Log(enemyUnits.Count);

		player.SetActive (true);

		for (int j = 0; j < enemyUnits.Count; j++) {
			enemyUnits [j].SetActive (true);
		}

		player.GetComponent<Player> ().SetMovement ();
	}



	void OnApplicatoinQuit() {
		PlayerPrefs.DeleteAll ();
		Debug.Log ("delete prefs");
	}

}