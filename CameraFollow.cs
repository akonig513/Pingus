using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject Player;
	public float smoothMove;

	// Use this for initialization
	void Start () {
		float xCamCoord = transform.position.x;
		transform.position = new Vector3 (xCamCoord, 0.0f, -10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		float xCamCoord = transform.position.x;
		float xcoord = Player.transform.position.x;
		if (xcoord > (xCamCoord + 2.0f)) {
			transform.position = new Vector3(xcoord - smoothMove, 0.0f, -10.0f);
		}
		if (xcoord < (xCamCoord - 2.0f)) {
			transform.position = new Vector3(xcoord + smoothMove, 0.0f, -10.0f);
		}
	}

	void LateUpdate(){
		float xCamCoord = transform.position.x;
		float playerY = Player.transform.position.y;
		if (playerY >= 0) {
			transform.position = new Vector3 (xCamCoord, playerY, -10.0f);
		}
	}
}
