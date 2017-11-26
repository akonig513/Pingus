using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TouchControls : MonoBehaviour {

	public PlayerController thePlayer;
	bool movingRight = false;
	bool movingLeft = false;
	bool stopMoving = false;

	// Use this for initialization
	void Start ()
	{
		thePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (movingRight) {
			RightArrow ();
		}
		if (movingLeft) {
			LeftArrow ();
		}
		if (stopMoving) {
			UnpressedArrow ();
		}
	}

		public void LeftArrow()
		{
			thePlayer.move (-1);
			movingLeft = true;
			movingRight = false;
			stopMoving = false;
		}

		public void RightArrow()
		{
			thePlayer.move (1);
			movingRight = true;
			movingLeft = false;
			stopMoving = false;
		}

		public void UnpressedArrow()
		{
			movingRight = false;
			movingLeft = false;
			stopMoving = true;
		}

		public void Jump()
		{
			thePlayer.jump();
		}
}
