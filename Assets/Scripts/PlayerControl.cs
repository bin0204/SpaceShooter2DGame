using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public GameObject GameManagerGO;

	public GameObject PlayerBulletGO;
	public GameObject bulletPosition01;
	public GameObject bulletPosition02;
	public GameObject ExplosionGO;


	//ui

	public Text LivesUIText;

	const int MaxLives = 3;
	int lives;


	//for the speed of my player
	public float speed;


	public void Init()
	{
		lives = MaxLives;

		LivesUIText.text = lives.ToString ();
		gameObject.SetActive (true);
	}
	// Use this for initialization

	void Start () {

	}
	// Update is called once per frame
	void Update () {

		//fire bullets when the spacebar is pressed
		if(Input.GetKeyDown("space")){
			GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
			bullet01.transform.position = bulletPosition01.transform.position;

			GameObject bullet02 = (GameObject)Instantiate (PlayerBulletGO);
			bullet02.transform.position = bulletPosition02.transform.position;
		}
		//the value will be -1, 0, or 1 (for left, no input, and right)
		float x = Input.GetAxisRaw ("Horizontal");
		//the value will be -1, 0, or 1 (for down, no input, and up)
		float y = Input.GetAxisRaw ("Vertical");

		//now based on the input we compute a direction vector, and we normalize it to get a unit vector
		Vector2 direction = new Vector2(x,y).normalized;

		//now we call the function that computes and sets the player's position
		Move (direction);
	}

	void Move(Vector2 direction)
	{
		//find the screen limits to the player's movement (left, right, and bottom edeges of the screen)
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0)); // this is the bottom-left point(corner) of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		max.x = max.x - 0.2f; //subtract the player sprite half width
		min.x = min.x + 0.2f; //add the player sprite half width

		max.y = max.y - 0.255f;//substract the player sprite half height
		min.y = min.y + 0.255f;//add the player sprite half height

		//get the player's current position
		Vector2 pos = transform.position;

		//calculate the new position
		pos += direction * speed * Time.deltaTime;

		//make sure the new position is not outside the screen
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		//update the player's position
		transform.position = pos;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//detect collision of the player ship with enemy ship
		if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag")) {
			PlayExplosion ();
			lives--;
			LivesUIText.text = lives.ToString ();

			if (lives == 0) {

				//game over
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
				//hide the player's ship
				gameObject.SetActive(false);
			}

		}
	}
	void PlayExplosion()
	{
		GameObject explosion = (GameObject)Instantiate (ExplosionGO);

		explosion.transform.position = transform.position;
	}
}
