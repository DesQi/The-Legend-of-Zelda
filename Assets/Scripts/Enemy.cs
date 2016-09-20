using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour {

	public float walking_velocity = 1.0f;
	public float knockback_velocity = 8.0f;
	public int damaged_times = -1;

	public int sword_damage = 1;
	public int bomb_damage = 1;
	public int boomerang_damage = 1;
	public int bow_damage = 1;

	public int enemy_health;

	public EntityState current_state = EntityState.NORMAL;
	public Direction current_direction = Direction.SOUTH;
	public Direction knockback_direction = Direction.OTHER;

	public float current_dir_value = 0.0f;

	public GameObject[] prefabPickUps;
	public float pickUpDropChance = 0.5f;

	// Use this for initialization
	void Start () {
	
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
//		else {
//			Move ();
//		}
	}

	// Trigger with weapons
	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Block") {
			GetComponent<Rigidbody>().velocity = Vector3.zero;
//			GridBasedMove ();
			ChooseDirectionMove (current_dir_value + 0.25f);
		}
		else if (coll.gameObject.tag == "Weapon_Sword") {
			Debug.Log ("trigger sword");
			Direction weapon_direction = coll.GetComponent<Weapon> ().weapon_direction;
			EnemyDmaged (sword_damage, weapon_direction);
		} else if (coll.gameObject.tag == "Bomb") {
			Direction weapon_direction = coll.GetComponent<Weapon> ().weapon_direction;
			EnemyDmaged (bomb_damage, weapon_direction);
		} else if (coll.gameObject.tag == "Boomerang") {
			Direction weapon_direction = coll.GetComponent<Weapon> ().weapon_direction;
			EnemyDmaged (boomerang_damage, weapon_direction);
		} else if (coll.gameObject.tag == "Bow") {
			Direction weapon_direction = coll.GetComponent<Weapon> ().weapon_direction;
			EnemyDmaged (bow_damage, weapon_direction);
		}  else {
			Debug.Log ("trigger other");
		}
		// TODO: other collision
	}

//	void OnCollisionEnter(Collision  coll) {
//		if (coll.gameObject.tag == "Block") {
//			Debug.Log ("trigger block");
//			GridBasedMove ();
//			ChooseDirectionMove ();
//		}
//	}

	// Cause damage to the enemy, and change the movement
	public void EnemyDmaged(int damage, Direction weapon_dir) {
		if (current_state != EntityState.DAMAGED) {
			current_state = EntityState.DAMAGED;
			enemy_health -= damage;
			if (enemy_health <= 0) {
				DropPickUps ();
				// TODO: destroy the enemy
			} else {
				// Enemy nockback
				damaged_times = 10;
				if (current_direction == Direction.NORTH || current_direction == Direction.SOUTH) {
					if (weapon_dir == Direction.NORTH || weapon_dir == Direction.SOUTH) {
						Debug.Log ("knockback");
						knockback_direction = weapon_dir;
						Debug.Log (knockback_direction);
					} 
				} else if (current_direction == Direction.EAST || current_direction == Direction.WEST) {
					if (weapon_dir == Direction.EAST || weapon_dir == Direction.WEST) {
						knockback_direction = weapon_dir;
					}
				} 
			}
		}
	}

	// Move enemy
	public virtual void Move() {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0) 
			* knockback_velocity * Time.deltaTime;
	}

	// Enemy knock back when damaged
	public virtual void KnockBackMove() {
		if (knockback_direction == Direction.NORTH) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 1, 0) 
				* knockback_velocity;
		} else if (knockback_direction == Direction.SOUTH) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (0, -1, 0) 
				* knockback_velocity;
		} else if (knockback_direction == Direction.EAST) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (1, 0, 0) 
				* knockback_velocity;
		} else if (knockback_direction == Direction.WEST) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (-1, 0, 0) 
				* knockback_velocity;
		}
	}

	// Drop ruppe, heart, key, etc
	public void DropPickUps() {
		// Potentially generate a PowerUp
//		if (UnityEngine.Random.value <= pickUpDropChance) {
			int ndx = UnityEngine.Random.Range(0,prefabPickUps.Length); 
			Instantiate( prefabPickUps[ndx], transform.position, Quaternion.identity ); 
//		}
	}

	// Enemy must move in grid
	// Link can move in the middle of two grids, but enemies can't
	// So enemy movement is % 1.0f but link is % 0.5f
	public void GridBasedMove(bool change_x = true) {
		Vector3 pos = transform.position;
			float x_offset = pos.x % 1.0f;
			double pos_x_delta = pos.x - Math.Truncate (pos.x);
			if (pos_x_delta <= 0.5f) {
				pos.x -= x_offset;
			} else {
				pos.x += (1.0f - x_offset);
			}

			float y_offset = pos.y % 1.0f;
			double pos_y_delta = pos.y - Math.Truncate (pos.y);
			if (pos_y_delta <= 0.5f) {
				pos.y -= y_offset;
			} else {
				pos.y += (1.0f - y_offset);
			}

		transform.position = pos;
	}

	// Some enemies may change their directions randomly
	public void ChooseDirectionMove(float new_dir = -1.0f) {
		Direction new_direction = current_direction;
		if (new_dir == -1.0f) {
			new_dir = UnityEngine.Random.value;
		} else {
			new_dir = new_dir % 1.0f;
		}
		
		if (new_dir < 0.25f) {
			new_direction = Direction.NORTH;
			GridBasedMove ();
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 1, 0)
			* walking_velocity;
		} else if (new_dir < 0.50f) {
			new_direction = Direction.SOUTH;
			GridBasedMove ();
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (0, -1, 0)
			* walking_velocity;
		} else if (new_dir < 0.75f) {
			new_direction = Direction.EAST;
			GridBasedMove ();
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (1, 0, 0)
			* walking_velocity;
		} else {
			new_direction = Direction.WEST;
			GridBasedMove ();
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (-1, 0, 0)
			* walking_velocity;
		} 
		current_direction = new_direction;
		current_dir_value = new_dir;
	}
}
