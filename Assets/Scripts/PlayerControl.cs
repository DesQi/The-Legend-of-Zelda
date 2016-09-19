using UnityEngine;
using System.Collections;

public enum Direction {NORTH, EAST, SOUTH, WEST, OTHER};
public enum EntityState {NORMAL, ATTACKING, DAMAGED};

public class PlayerControl : MonoBehaviour {

	public float walking_volocity = 1.0f;

	//HUD
	public int max_heart = 6;
	public int rupee_count = 0;
	public int key_count = 0;
	public int heart_count = 4;
	public int bomb_count = 0;

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
		Application.targetFrameRate = 60;

		if (instance != null)
			Debug.LogError ("Multiple Link objects detected!");
		instance = this;

		this.transform.position = new Vector3 (39.5f, 2.0f, 0);

		animation_state_machine = new StateMachine ();
		animation_state_machine.ChangeState (new StateIdleWithSprite (this, GetComponent<SpriteRenderer> (), link_run_down [0]));

		control_state_machine = new StateMachine ();
		control_state_machine.ChangeState (new StateLinkNormalMovement (this));
	}
	
	// Update is called once per frame
	void Update () {
		// Update the animation state machine
		animation_state_machine.Update ();

		//Update the movement state machine
		// If this state machine is ever "idle", we give it a standard movement state
		control_state_machine.Update();
		if (control_state_machine.IsFinished ())
			control_state_machine.ChangeState (new StateLinkNormalMovement (this));

	}

	void OnTriggerEnter(Collider coll) {
		// If trigger with enemy
		if (current_state != EntityState.DAMAGED) {
			if (coll.gameObject.tag == "Stalfos") {
				Debug.Log ("stalfos");
				control_state_machine.ChangeState (new StateLinkDamaged (this, "Stalfos"));
			}

		}

		if (coll.gameObject.tag == "Rupee") {
			Destroy (coll.gameObject);
			rupee_count++;
		} else if (coll.gameObject.tag == "Heart") {
			Destroy (coll.gameObject);
			if (heart_count < max_heart)
				heart_count++;
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "Key") {
			Destroy (coll.gameObject);
			key_count++;
		} else if (coll.gameObject.tag == "Bomb") {
			Destroy (coll.gameObject);
			bomb_count++;
		} else if (coll.gameObject.tag == "Key") {
			Destroy (coll.gameObject);
			key_count++;
		} else if (coll.gameObject.tag == "Door") {
			// haha
			control_state_machine.ChangeState (new StateLinkTriggerDoor (this));
		} 
	}
		
}
