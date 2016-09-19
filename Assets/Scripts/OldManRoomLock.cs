using UnityEngine;
using System.Collections;

public class OldManRoomLock : MonoBehaviour {

	// instance
	public static OldManRoomLock instance;

	// Use this for initialization
	void Start () {
		if (instance != null) {
			Debug.LogError ("Multiple OldManRoomLock!");
		}
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
