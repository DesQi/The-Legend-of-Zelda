using UnityEngine;
using System.Collections;

public class EnemyStalfos : Enemy {
	public float changeDirectionChance = 0.005f;
	public Sprite[] stalfos_sprites;
	public int current_frame_index = 0;

	// Use this for initialization
	void Start () {
		walking_velocity = 2.0f;
		knockback_velocity = 8.0f;
		enemy_health = 3;
		GridBasedMove ();
		ChooseDirectionMove ();
	}

	// Update is called once per frame
	void Update () {
		if (damaged_times > 0) {
			KnockBackMove ();
			damaged_times--;
		} else if (damaged_times == 0) {
			knockback_direction = Direction.OTHER;
			current_state = EntityState.NORMAL;
			damaged_times--;
		} 
	}


	void FixedUpdate() {
		if (current_frame_index % 20 < 10) {
			this.GetComponent<SpriteRenderer> ().sprite = stalfos_sprites [0];
		} else {
			this.GetComponent<SpriteRenderer> ().sprite = stalfos_sprites [1];
		}
		current_frame_index++;
		if (UnityEngine.Random.value < changeDirectionChance) {
			GridBasedMove ();
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			ChooseDirectionMove ();
		}
	}
}
