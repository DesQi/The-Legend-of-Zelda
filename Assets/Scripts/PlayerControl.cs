using UnityEngine;
using System.Collections;

public enum Direction {NORTH, EAST, SOUTH, WEST};
public enum EntityState {NORMAL, ATTACKING};

public class PlayerControl : MonoBehaviour {

	// public hero_variables
	public float walking_velocity = 1.0f;
	public int rupee_count = 0;
	public float heart_count = 3.0f;

	// instance
	public static PlayerControl instance;

	public Sprite[] link_run_down;
	public Sprite[] link_run_up;
	public Sprite[] link_run_right;
	public Sprite[] link_run_left;

	StateMachine animation_state_machine;
	StateMachine control_state_machine;
	
	public EntityState current_state = EntityState.NORMAL;
	public Direction current_direction = Direction.SOUTH;

	public GameObject selected_weapon_prefab;

	// Use this for initialization
	void Start () {
		if (instance != null)
			Debug.LogError("Multiple link objects detected!"); 
		instance = this;
		animation_state_machine = new StateMachine();
		animation_state_machine.ChangeState(new StateIdleWithSprite(this,GetComponent<SpriteRenderer>(), link_run_down[0]));
	}
	
	// Update is called once per frame
	void Update () {
		animation_state_machine.Update ();

		float horizontal_input = Input.GetAxis ("Horizontal");
		float vertical_input = Input.GetAxis ("Vertical");

		if (horizontal_input != 0.0f)
			vertical_input = 0.0f;

		GetComponent<Rigidbody> ().velocity = new Vector3 (horizontal_input, vertical_input, 0) * walking_velocity;
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Rupee") {
			rupee_count++;
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "Heart") {
			//DO Heart Action	
		}
	}
}
