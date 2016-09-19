using UnityEngine;
using System.Collections;

public class WoodSwordWeapon : Weapon {

	// Use this for initialization
	void Start () {
		weapon_direction = PlayerControl.instance.current_direction;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
