using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int sword_damage = 1;
	public int bomb_damage = 1;
	public int boomerang_damage = 1;
	public int bow_damage = 1;

	public int enemy_health;

	public EntityState current_state = EntityState.NORMAL;
	public Direction current_direction = Direction.SOUTH;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Trigger with weapons
	void OnTriggerEnter(Collider coll) {
		Direction weapon_direction = coll.GetComponent<Weapon> ().weapon_direction;
		if (coll.gameObject.tag == "Sword") {
			EnemyDmaged (sword_damage, weapon_direction);
		} else if (coll.gameObject.tag == "Bomb") {
			EnemyDmaged (bomb_damage, weapon_direction);
		} else if (coll.gameObject.tag == "Boomerang") {
			EnemyDmaged (boomerang_damage, weapon_direction);
		} else if (coll.gameObject.tag == "Bow") {
			EnemyDmaged (bow_damage, weapon_direction);
		}
		// TODO: other collision
	}

	// Cause damage to the enemy, and change the movement
	public void EnemyDmaged(int damage, Direction dir) {
		if (current_state != EntityState.DAMAGED) {
			current_state = EntityState.DAMAGED;
			enemy_health -= damage;
			if (enemy_health <= 0) {
				DropPickUps ();
				// TODO: destroy the enemy
			} else {
				if (current_direction == Direction.NORTH || current_direction == Direction.SOUTH) {
					
				} else if (current_direction == Direction.EAST || current_direction == Direction.WEST) {

				} 
			}
		}
	}

	// Move enemy
	public virtual void Move() {
		
	}

	// Drop ruppe, heart, key, etc
	public void DropPickUps() {
	
	}
}
