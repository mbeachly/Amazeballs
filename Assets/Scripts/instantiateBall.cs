using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class creates 2D balls that fall down
// where users click on the start screen
public class instantiateBall : MonoBehaviour
{
	public GameObject BallObj;
	
    // Start is called before the first frame update
    public void Start()
    {	

	}

    // Update is called once per frame
 
	public void Update()
	{
		if (Input.GetButtonDown("Fire1"))
        {
				Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(BallObj, worldPosition, Quaternion.identity);
        }
	}
}
