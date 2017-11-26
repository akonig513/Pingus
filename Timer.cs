using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public Text timerWin;
	private bool runclock = true;

	// TIMER counting UP
	float timer = 0.0f;

	void Update ()
	{
		if (runclock) {
			timer += Time.deltaTime;
			timer = Mathf.Round(timer * 100f) / 100f;
			timerWin.text = "Time: " + timer;
		}

	}
}
