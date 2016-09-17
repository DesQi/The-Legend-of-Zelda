using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	// When hit RightLeftDoor/UpDownDoor, change camera position based on link's direction
	public float camera_speed = 0.5f;

	// Camera Position
	public Vector3 curr_cam_pos = Vector3.zero;
	public Vector3 new_cam_pos = Vector3.zero;

	// Player Position
	public StateMachine player_move_statemachine;

	// Camera Move Direction
	public string cam_direction = "";

	// Use this for initialization
	void Start () {
		curr_cam_pos = Camera.main.transform.position;
		new_cam_pos = curr_cam_pos;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (cam_direction == "LEFT") {
			if (curr_cam_pos.x > new_cam_pos.x) {
				curr_cam_pos.x -= camera_speed;
				Camera.main.transform.position = curr_cam_pos;
			} else {
				Camera.main.transform.position = new_cam_pos;
				curr_cam_pos = new_cam_pos;
			}
		} else if (cam_direction == "RIGHT") {
		
		}
	}	

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Link") {
			if (PlayerControl.instance.current_direction == Direction.WEST) {
				// Link face to West, Camera Move Left
				cam_direction = "LEFT";
				new_cam_pos.x = curr_cam_pos.x - 16.0f;
			}
		}
	}
}
