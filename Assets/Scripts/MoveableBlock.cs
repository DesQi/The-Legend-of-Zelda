using UnityEngine;
using System.Collections;

//Block only moveable when room is cleared and block has not been moved
//Two blocks only can be moved to different direction
//OldManRoom Door opens
public class MoveableBlock : MonoBehaviour {
	// Variables
	public float moveSpeed = 1.0f;
	public bool hasNotBeenMoved = true;
	public bool startToMove = false;
	public bool roomCleared = false;
	public Direction moveDirection = Direction.WEST;
	public Vector3 blockPosition;
	public Vector3 currPosition;
	public Vector3 newPosition;
	public Vector3 collisionCameraPosition;
	public Vector3 currCameraPosition;


	// Use this for initialization
	void Start () {
		blockPosition = transform.position;
		currPosition = blockPosition;
		newPosition = currPosition;
	}

	// Update is called once per frame
	void Update () {
		// update when room is cleared, change room cleared status
		// update camera position
		// room need to do...
		currCameraPosition = Camera.main.transform.position;
		// if cameraposition changes, reset the block, close the geldoor
		if (currCameraPosition != collisionCameraPosition) {
			hasNotBeenMoved = true;
			startToMove = false;
			this.transform.position = blockPosition;
		}
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.transform.tag == "Link" && hasNotBeenMoved && roomCleared && !startToMove) {
			collisionCameraPosition = Camera.main.transform.position;
			currPosition = blockPosition;
			if (PlayerControl.instance.current_direction == moveDirection) {
				if (moveDirection == Direction.WEST) {
					newPosition.x = blockPosition.x - 1.0f;
					startToMove = true;
				} else if (moveDirection == Direction.NORTH) {
					newPosition.y = blockPosition.y + 1.0f;
					startToMove = true;
				}
			}
		}
	}

	void FixedUpdate() {
		if (hasNotBeenMoved && startToMove) {
			if (moveDirection == Direction.WEST) {
				if (currPosition.x > newPosition.x) {
					currPosition.x -= moveSpeed;
					this.transform.position = currPosition;
				} else {
					// open the geldoor
					this.transform.position = newPosition;
					hasNotBeenMoved = false;
					startToMove = false;
					if (OldManRoomLock.instance) 
						Destroy (OldManRoomLock.instance.gameObject);
				}
			} else if (moveDirection == Direction.NORTH) {
				if (currPosition.y < newPosition.y) {
					currPosition.y += moveSpeed;
					this.transform.position = currPosition;
				} else {
					this.transform.position = newPosition;
					hasNotBeenMoved = false;
					startToMove = false;
				}
			}
		}
	}
}
