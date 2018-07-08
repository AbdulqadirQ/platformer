using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// determines how fast camera goes back to player
	public float resetSpeed = 0.5f;
	public float cameraSpeed = 0.3f;

	public Bounds cameraBounds;

	// target will be player, since player's transform will be used to follow player
	private Transform target;

	private float offsetZ;
	private Vector3 lastTargetPosition;
	private Vector3 currentVelocity;

	private bool foolowsPlayer;

	void Awake(){
		BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
		// resizes box collider
		myCollider.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
		cameraBounds = myCollider.bounds;
	}

	void Start () {
		target = GameObject.FindGameObjectWithTag (Tags.PLAYER_TAG).transform;
		lastTargetPosition = target.position;
		// gets z-axis offset of camera in relation to player
		offsetZ = (transform.position - target.position).z;
		foolowsPlayer = true;
	}
	
	void FixedUpdate () {
		if(foolowsPlayer){
			Vector3 aheadTargetPos = target.position + Vector3.forward * offsetZ;

			// only follows player if he goes forward
			if(aheadTargetPos.x >= transform.position.x){
				// moves transform.position (i.e. camera game object) towards 
				// aheadTargetPos
				// currentVelocity is irrelevant - only used to satisfy function parameters
				// cameraSpeed used as the velocity of the camera
				Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position,
				aheadTargetPos, ref currentVelocity, cameraSpeed);

				// camera's transform is changed, except within y-axis
				transform.position = new Vector3(newCameraPosition.x, transform.position.y,
					newCameraPosition.z);

				lastTargetPosition = target.position;
			}
		}
	}
}
