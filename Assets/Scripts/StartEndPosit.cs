using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndPosit : MonoBehaviour
{
    public GameObject CanvasObjStart;
	public GameObject CanvasObjStop;
	public GameObject StartObj;
	public GameObject PlayerObj;
	public GameObject EndObj;
	private static Vector3 startPosition;
	private static Vector3 endPosition;
	
	// Start is called before the first frame update
    void Start()
    {
		// Creates "pick start point"  prompt on screen
		GameObject StartText = Instantiate(CanvasObjStart, this.transform, false) as GameObject;
		StartText.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    // Update is called once per frame
    void Update()
    {
		// On initial click:
		if (Globals.isShowing == 0 && Input.GetMouseButtonDown(0))
		{
			
			// Delete player start position prompt
			Destroy(GameObject.FindGameObjectWithTag("StartObj"));
			
			// Creates "pick end point" prompt on screen
			Instantiate(CanvasObjStop, this.transform, false);
			//StopText.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
			
			// Store start position selected by user
			startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// Set Y value to 0 otherwise it uses Y value of camera
			startPosition = new Vector3(startPosition.x, 0, startPosition.z);

			// Place start position icon (Green texture)
			
			
			
			// Signal variable indicates user selected start point
			Globals.isShowing = 1;
		}	
		
		// On second click
		else if (Globals.isShowing == 1 && Input.GetMouseButtonDown(0))
		{
			// Disables future selection options
			Globals.isShowing = 2;
			
			// Store end point selected by user
			endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// Set Y value to 0 otherwise it uses Y value of camera
			endPosition = new Vector3(endPosition.x, 0, endPosition.z);


			// Delete player end position prompt
			Destroy(GameObject.FindGameObjectWithTag("StopObj"));
			
			// After user selects start game
		
			// Create player and end objects and place on board
			GameObject endPoint = Instantiate(EndObj, endPosition, Quaternion.identity) as GameObject;

			GameObject player = Instantiate(PlayerObj, startPosition, Quaternion.identity) as GameObject;
		}
		
    }
	
}
