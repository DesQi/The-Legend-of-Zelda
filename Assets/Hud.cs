using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hud : MonoBehaviour {

	public Text rupee_text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int num_player_ruppes = PlayerControl.instance.ruppe_count;
		rupee_text.text = "rupees: " + num_player_ruppes;
	}
}
