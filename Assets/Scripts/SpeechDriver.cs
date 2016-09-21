using UnityEngine;
using System.Collections;

public class SpeechDriver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Link" && PlayerControl.instance.current_direction == Direction.WEST) {
			OldManRoomSpeech.instance.speechTrigger = true;
		} else {
			OldManRoomSpeech.instance.speechTrigger = false;
		}
	}
}
