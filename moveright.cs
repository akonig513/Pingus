using UnityEngine;
using System.Collections;

public class moveright : MonoBehaviour {

	public float speed;
	private float factor = 0.0001f;


	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (speed, 0.0f, 0.0f) * factor;
	}
}
