using UnityEngine;
using System.Collections;

public class player_control : MonoBehaviour {

	public float walking_volocity = 1.0f;
	public int ruppe_count = 0;

	public static player_control instance;

	// Use this for initialization
	void Start () {
		if (instance != null)
			Debug.LogError ("Multiple Link objects detected!");
		instance = this;
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
		if (coll.gameObject.tag == "Rupee") {
			Destroy (coll.gameObject);
			ruppe_count++;
		} else if (coll.gameObject.tag == "Heart") {
			// haha
		}
	}
}
