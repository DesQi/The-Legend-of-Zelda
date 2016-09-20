using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider coll) {
		if (coll.gameObject.tag == "Link") {
			Debug.Log ("link enter");
		}
	}

	void OnTriggerExit (Collider coll) {
		if (coll.gameObject.tag == "Link") {
			Debug.Log ("link exit");
		}

	}
}
