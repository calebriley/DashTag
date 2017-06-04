using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public int value;

	void OnTriggerEnter2D(Collider2D other){
		other.GetComponent<PlayerController>().IncreaseScore(value);
		Destroy(this.gameObject);
	}
}
