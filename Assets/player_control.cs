using UnityEngine;
using System.Collections;

public class player_control : MonoBehaviour {

	public float walking_volocity = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal_input =  Input.GetAxis ("Horizontal");
		float vertical_input =  Input.GetAxis ("Vertical");

		if (horizontal_input != 0.0f)
			vertical_input = 0.0f;

		GetComponent<Rigidbody> ().velocity = new Vector3 (horizontal_input, vertical_input, 0) * walking_volocity;
	}

	void OnTriggerEnter(Collider coll) {
		print (coll.gameObject.name);
	}
}
