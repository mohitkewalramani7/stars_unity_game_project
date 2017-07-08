/**

The script that will be attahced to the Unity Player Object
This script controls the player's behaviour when given
an input from accelorometer as well as how it makes the
collectible stars disappear.

@author 	Mohit Kewalramani
@published 	2017-07-07
@version	1.0

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
The class that controlls the PlayerController behaviour
 */
public class PlayerController : MonoBehaviour {

	private Rigidbody rb; 				// The player object
	
	public float speed;					// The speed with with the player object moves
	private Quaternion calibrationQuaternion;	// The input given in from the accelorometer
	
	private int score;		// The integer value that stores the user's score
	public Text scoreText;	// The text view on the UI that displays the user's score
	
	private float timeLeft = 120.0f;	// The time left until the current game is over
	public Text timeText;	// The textview on the screen that displays the amount of time left
	
	public Text gameOverText;	// The text displayed on the screen when the game is over
	public Button reset;	// The button to be clicked to reset the game
	private bool gameOver = false;	// The local boolean used to keep track of the game's status 
	List<Collider> objects = new List<Collider>(); // A list of colliders the player has collected

	/**
	This method is called at the start of the game.
	 */
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		CalibrateAccelerometer();
		gameOverText.text = "";		// Sets the Game Over text to blank
		
		// Set up reset button and adds a listener
		Button resetButton = reset.GetComponent<Button>();
		resetButton.onClick.AddListener(resetGame);
	}

	/**
	This method is called at the start of every frame.
	 */
	void Update(){
		if ((Mathf.Round(timeLeft) > 0)){
			// The game is not over. Reduce time available by 1 second and update UI.
			timeLeft = timeLeft - Time.deltaTime;
			timeText.text = "Time Left : " + Mathf.Round(timeLeft).ToString() + " s";
		}
		else{
			// Game over. Print game over to UI and display final score
			gameOver = true;
			scoreText.text = "Your Score : " + score.ToString();
			gameOverText.text = "Game Over";
		}
	}

	/**
	This method is called at the start of every frame, and performs 
	the Physics calculation to advance the player around the game.
	 */
	void FixedUpdate()
	{	
		// Collect data from accelorometer
		Vector3 accelerationRaw = Input.acceleration;
		Vector3 acceleration = FixAcceleration(accelerationRaw);

		// Move the ball horizontally, but not vertically.
		Vector3 movement = new Vector3(acceleration.x, 0, acceleration.y);

		// Apply fore to rigidbody
		rb.AddForce(movement * speed);
	}

	/**
	A method that updates determines the behaviour when the ball collides
	with a Star
	 */
	void OnTriggerEnter(Collider other){
		// Check if it's a star
		if (other.gameObject.CompareTag("Star") && !gameOver)
		{
			objects.Add(other);							// Add to collected list
			other.gameObject.SetActive(false);			// Make star disappear
			score += 1;									// Add 1 to score
			scoreText.text = "Score : " + score.ToString();		// Update score on screen
		}
	}

	/**
	Resets the game. Method is called once the button is clicked 
	on screen
	 */
	void resetGame(){
		timeLeft = 120.0f;	// Resets the time left (120 seconds)
		score = 0;			// Resets the score to 0
		scoreText.text = "Your Score : " + score.ToString(); // Resets the Text showing the score
		
		// Sets the implicit boolean of gameOver back to false
		gameOver = false;	

		// Iterates over all collected stars, and sets them all to be active
		// so they can be collected again								
		for (int i = 0; i < objects.Count; i++){
			objects[i].gameObject.SetActive(true);
		}
	}

	/**
	This method calibrates the accelerometer at the start of the game
	 */
	void CalibrateAccelerometer(){
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
	}

	/**
	Normalizes the acceleration based on the input given by
	the accelerometer

	@return Vector3 (A 3D vector of the acceleration)
	 */
	Vector3 FixAcceleration (Vector3 acceleration){
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}
}
