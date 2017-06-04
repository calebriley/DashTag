using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float moveThreshold;
	public float speed;

	public float dashBar;
	public float dashRecovery;
	public float dashLength;
	public float dashFactor;

	public GameObject dashDisplay;
	public GameObject scoreNumber;

	float xMovement;
	float yMovement;
	Vector3 newMove;

	float currentBar;
	float currentDash;
	int score = 0;

	Vector3 pos;

	public enum Player {
		One, 
		Two
	}

	public Player playerNumber;

	// Use this for initialization
	void Start () {
		dashBar += 0.1f;
		currentBar = dashBar;
		currentDash = 1.0f;
	
	}
	
	// Update is called once per frame
	void Update () {
		
//		if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > moveThreshold){
//			xMovement = Input.GetAxisRaw("Horizontal");
//		} else {
//			xMovement = 0.0f;
//		}
//
//		if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > moveThreshold){
//			yMovement = Input.GetAxisRaw("Vertical");
//		} else {
//			yMovement = 0.0f;
//		}

		if (playerNumber == Player.One){
			if (Input.GetKey("a")){
				xMovement = -1.0f;
			} else if (Input.GetKey("d")){
				xMovement = 1.0f;
			} else {
				xMovement = 0.0f;
			}
			if (Input.GetKey("s")){
				yMovement = -1.0f;
			} else if (Input.GetKey("w")){
				yMovement = 1.0f;
			} else {
				yMovement = 0.0f;
			}
		} else {
			if (Input.GetKey("left")){
				xMovement = -1.0f;
			} else if (Input.GetKey("right")){
				xMovement = 1.0f;
			} else {
				xMovement = 0.0f;
			}
			if (Input.GetKey("down")){
				yMovement = -1.0f;
			} else if (Input.GetKey("up")){
				yMovement = 1.0f;
			} else {
				yMovement = 0.0f;
			}
		}



		newMove = new Vector3(xMovement, yMovement);
		newMove.Normalize();

		if (playerNumber == Player.One){
			if (Input.GetKey("space") && currentBar > 1.0f){
				currentBar =- 1.0f;
				currentDash = dashFactor;
			}
		} else {
			if (Input.GetKey("return") && currentBar > 1.0f){
				currentBar =- 1.0f;
				currentDash = dashFactor;
			}
		}

		if (currentBar < 1.0f){
			currentBar += dashRecovery * Time.deltaTime;
		}


		newMove = newMove * speed * Time.deltaTime * currentDash;

		currentDash = Mathf.Lerp(currentDash, 1.0f, dashLength * Time.deltaTime);

		transform.Translate(newMove);

		dashDisplay.GetComponent<Slider>().value = currentBar;
		scoreNumber.GetComponent<Text>().text = score.ToString();

		pos = Camera.main.WorldToViewportPoint(transform.position);
		pos.x = Mathf.Clamp01(pos.x);
		pos.y = Mathf.Clamp01(pos.y);
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	public void IncreaseScore(int valueAdded){
		this.score += valueAdded;
	}
}
