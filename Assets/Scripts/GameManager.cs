using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject playButton;
	public GameObject playerShip;
	//public GameObject enemySpawner;
	// Use this for initialization

	public enum GameManagerState
	{
		Opening,
		Gameplay,
		GameOver,
	}

	GameManagerState GMState;

	void Start () {
		GMState = GameManagerState.Opening;
	}
	
	// Update is called once per frame
	void UpdateGameManagerState() {
		switch (GMState) {
		case GameManagerState.Opening:
			break;
		case GameManagerState.Gameplay:

			break;
		case GameManagerState.GameOver:

			break;
		}
	}
	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
		UpdateGameManagerState ();
	}
}
