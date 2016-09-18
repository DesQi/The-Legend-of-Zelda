using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hud : MonoBehaviour {

	// Small hud texts
	public Text rupee_text;
	public Text key_text;
	public Text heart_text;
	public Text bomb_text;

	// Use this for initialization
	void Start () {
		//rupee_count
		int num_player_rupees = PlayerControl.instance.rupee_count ; 
		rupee_text.text = "Rupees: " +  num_player_rupees.ToString ();
		//key_count
		int num_player_keys = PlayerControl.instance.key_count;
		key_text.text = "Keys: " + num_player_keys;
		//heart_count
		int num_player_hearts = PlayerControl.instance.heart_count;
		heart_text.text = "Hearts: " + num_player_hearts;
		//bomb_count
		int num_player_bombs = PlayerControl.instance.bomb_count;
		bomb_text.text = "Bombs: " + num_player_bombs;
	}
	
	// Update is called once per frame
	void Update () {
		//rupee_count
		int num_player_rupees = PlayerControl.instance.rupee_count ; 
		rupee_text.text = "Rupees: " +  num_player_rupees.ToString ();
		//key_count
		int num_player_keys = PlayerControl.instance.key_count;
		key_text.text = "Keys: " + num_player_keys;
		//heart_count
		int num_player_hearts = PlayerControl.instance.heart_count;
		heart_text.text = "Hearts: " + num_player_hearts;
		//bomb_count
		int num_player_bombs = PlayerControl.instance.bomb_count;
		bomb_text.text = "Bombs: " + num_player_bombs;

	}
}
