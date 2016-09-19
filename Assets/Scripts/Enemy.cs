using UnityEngine;
using System.Collections;

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
		} else {
			Move ();
		}
	}

	// Trigger with weapons
	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Weapon_Sword") {
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
		} else {
			Debug.Log ("trigger other");
		}
		// TODO: other collision
	}

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
	
	}
}
