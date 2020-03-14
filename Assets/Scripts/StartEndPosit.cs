using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if start and end points have been automatically detected
/// Instantiates text boxes to prompt user to manually pick start and end points 
/// Records positions that the user chooses for the start and end points 
/// Starts the game by instantiating the player ball and end point and starts the timer
/// </summary>
public class StartEndPosit : MonoBehaviour
{
    public GameObject CanvasObjStart;
	public GameObject CanvasObjStop;
	public GameObject StartObj;
	public GameObject PlayerObj;
	public GameObject EndObj;

	// Start is called before the first frame update
	void Start()
	{	// Check if start point is not detected
		if (Globals.pickStep == 0)
		{
			// Creates "pick start point"  prompt on screen
			//GameObject StartText = Instantiate(CanvasObjStart, this.transform, false) as GameObject;
			//StartText.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
			Instantiate(CanvasObjStart, this.transform, false);
		} // Check if end point is not detected
		else if (Globals.pickStep == 1)
		{
			Instantiate(CanvasObjStop, this.transform, false);
		} // Both start and end were automatically detected
		else if (Globals.pickStep == 2)
		{
			startGame();
		}
    }

    // Update is called once per frame
    void Update()
    {
		// On initial click:
		if (Globals.pickStep == 0 && Input.GetMouseButtonDown(0))
		{
			
			// Delete player start position prompt
			Destroy(GameObject.FindGameObjectWithTag("StartObj"));
			
			// Creates "pick end point" prompt on screen
			Instantiate(CanvasObjStop, this.transform, false);
			//StopText.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
			
			// Store start position selected by user
			Globals.startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// Set Y value to 0 otherwise it uses Y value of camera
			Globals.startPosition = new Vector3(Globals.startPosition.x, 0, Globals.startPosition.z);
			
			// Signal variable indicates user selected start point
			Globals.pickStep = 1;
		}	
		
		// On second click
		else if (Globals.pickStep == 1 && Input.GetMouseButtonDown(0))
		{
			// Disables future selection options
			Globals.pickStep = 2;
			
			// Store end point selected by user
			Globals.endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// Set Y value to 0 otherwise it uses Y value of camera
			Globals.endPosition = new Vector3(Globals.endPosition.x, 0, Globals.endPosition.z);


			// Delete player end position prompt
			Destroy(GameObject.FindGameObjectWithTag("StopObj"));

			startGame();
		}
		
    }

	void startGame()
	{
		// Create player and end objects and place on board
		GameObject endPoint = Instantiate(EndObj, Globals.endPosition, Quaternion.identity) as GameObject;
		GameObject player = Instantiate(PlayerObj, Globals.startPosition, Quaternion.identity) as GameObject;

		// Apply selected options size
		player.transform.localScale = new Vector3(Globals.ballSize, Globals.ballSize, Globals.ballSize);
		endPoint.transform.localScale = new Vector3(Globals.ballSize, Globals.ballSize, Globals.ballSize);

		// Apply selected options theme
		Texture2D ballTexture = Resources.Load(Globals.ballTexName) as Texture2D;
		player.GetComponent<Renderer>().material.SetTexture("_MainTex", ballTexture);

		// Get the timer object and attach the script to update it
		GameObject TimerController = new GameObject();
		TimerController.AddComponent<Timer>();
		Instantiate(TimerController);

		// Set playSavedGame back to false
		Globals.playSavedGame = false;
	}
	
}
