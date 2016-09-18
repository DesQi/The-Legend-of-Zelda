using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hud : MonoBehaviour {

	// Rupee_text
	public Text rupee_text;
	// Key_text
	public Text key_text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int num_player_rupees = PlayerControl.instance.rupee_count ; 
		rupee_text.text = "Rupees: " +  num_player_rupees.ToString ();
	}
}
