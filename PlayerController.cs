using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour {

	public float speed;
	public float height;
	//Game objects
	private Rigidbody2D rb2d;
	public Vector3 currentVelocity;
	public Vector3 storedVelocity;
	public Text winText;
	public bool CanMove = true;
	public bool grounded = false;
	public GameObject[] pickups;
	public GameObject pickupParent;
	public bool ended = false;

	//Audio Sources and Clips
	public AudioSource jumpSource;
	public AudioClip jumpSound;
	public AudioSource PickupSource;
	public AudioClip PickupSound;
	public AudioClip FinalPickupSound;
	public AudioSource DieSource;
	public AudioSource BoostSound;

	//Timer
	public Text timerText;
	float timer = 0.0f;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		pickups = new GameObject[pickupParent.transform.childCount];
		for (int i = 0; i < pickupParent.transform.childCount; i++)
		{
			pickups[i] = pickupParent.transform.GetChild(i).gameObject;
		}
	}
		

	void Update()
	{
		if (CanMove) {
			runTimer();
		}
		if (ended) {
			StartCoroutine(endOfLevel());
		}
	}
		
	void FixedUpdate ()
	{
			//Checking grounded state and updating current velocity
			if (rb2d.velocity.y == 0) {
				grounded = true;
			}
			currentVelocity = rb2d.velocity;

#if UNITY_STANDALONE || UNITY_WEBPLAYER
			//Controls
			move(Input.GetAxis ("Horizontal"));
			if (Input.GetKey (KeyCode.Space)) {
				jump();
			}
#endif
		
	}
	 

	IEnumerator endOfLevel(){
		print(Time.time);
		yield return new WaitForSeconds(4);
		print(Time.time);
		Application.LoadLevel(0);
	}

	IEnumerator SpeedBoostActive(){
		print(Time.time);
		winText.text = "SPEED BOOST";
		yield return new WaitForSeconds(1);
		winText.text = "";
		print(Time.time);
	}

	IEnumerator die(){
		print(Time.time);
		winText.text = "try again!";
		yield return new WaitForSeconds(3);
		print(Time.time);
		winText.text = "";
	}

	public void jump()
	{
		if (CanMove) {
			if (grounded) {
				rb2d.velocity = currentVelocity + new Vector3 (0, height);
				grounded = false;
				jumpSource.Play ();
			}
		}
	}

	public void move(float movementInput)
	{
		if (CanMove) {
			rb2d.velocity = new Vector2 (movementInput * speed, rb2d.velocity.y);
		}
	}

	public void runTimer()
	{
		timer += Time.deltaTime;
		timer = Mathf.Round (timer * 100f) / 100f;
		timerText.text = "Time: " + timer;
	}

	public void freezeTimer()
	{
		timerText.text = "Time: " + timer;
		CanMove = false;
		storedVelocity = currentVelocity;
		rb2d.velocity = new Vector3 (0,0,0);
		rb2d.isKinematic = true;
	}

	public void resumeTimer()
	{
		CanMove = true;
		rb2d.isKinematic = false;
		rb2d.velocity = storedVelocity;
	}
		

	//Functions for all collectibles
	void OnTriggerEnter2D(Collider2D other){
		//FOR WHEN YOU HIT THE GROUND
		if (other.gameObject.CompareTag("Ground")){
			grounded = true;
		}

		//FALL OFF STAGE
		if (other.gameObject.CompareTag("Pit")){
			transform.position = new Vector2(-6.46f,0.87f);
			rb2d.velocity = new Vector2 (0.0f, 0.0f);
			rb2d.freezeRotation = true;
			timer = 0.0f;
			speed = 10f;
			DieSource.Play ();
			for (int i = 0; i < pickups.Length; i++) {
				pickups[i].SetActive (true);
			}
			rb2d.freezeRotation = false;
			StartCoroutine(die());
		}

		//COLLECTING PICKUP
		if (other.gameObject.CompareTag ("Pickup")) {
			other.gameObject.SetActive(false);
			timer -= 0.5f;
			PickupSource.Play ();
		}

		// GET TO FINISH
		if (other.gameObject.CompareTag ("Goal")) {
			other.gameObject.SetActive(false);
			winText.text = "nice job!";
			rb2d.freezeRotation = true;
			rb2d.isKinematic = true;
			rb2d.velocity = new Vector2 (0.0f, 0.0f);
			CanMove = false;
			ended = true;
			PickupSource.clip = FinalPickupSound;
			PickupSource.Play ();
		}

		//HIT SPEED BOOST
		if (other.gameObject.CompareTag ("Speed Boost")) {
			speed = 15f;
			BoostSound.Play ();
			StartCoroutine (SpeedBoostActive());
			other.gameObject.SetActive(false);
		}
	}
}