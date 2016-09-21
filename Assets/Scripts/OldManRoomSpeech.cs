using UnityEngine;
using System.Collections;

public class OldManRoomSpeech : MonoBehaviour {

	public static OldManRoomSpeech instance;
	public bool speechTrigger = false;
	public string[] textArr;
	public int idxOfText = 0;
	public Vector3 OldManRoomCameraPos = new Vector3(7.51f, 39.41f, -10.0f);
	public int counter = 8;
	// Use this for initialization
	void Start () {
		if (instance != null)
			Debug.LogError ("Multiple speech instance.");
		instance = this;
		print (textArr.Length);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (speechTrigger == true && Camera.main.transform.position == OldManRoomCameraPos) {
			counter--;
			if (counter == 0)
				counter = 8;
			if (counter == 8 && textArr.Length > idxOfText) {
				if (idxOfText == 19) {
					this.GetComponent<TextMesh> ().text += "\n          "; 
				}
				this.GetComponent<TextMesh> ().text += textArr [idxOfText];
				idxOfText++;
			}
		} else {
			idxOfText=0;
			this.GetComponent<TextMesh> ().text = "";
			counter = 8;
		}
	}
}
