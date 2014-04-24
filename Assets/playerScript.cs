using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public Vector3 speed = new Vector3 (5f, 0f, 0f);
	public Vector3 jumpForce = new Vector3 (0f, 5f, 0f);
	public Vector3 boostForce = new Vector3 (0f, 0f, 5f);
	public float powerLvl = 0;

	private bool grounded = false;
	private bool dblJump = true;

	void Start(){
	}

	void FixedUpdate() {
		if (powerLvl > 0){
			if (Input.GetKey(KeyCode.X)){
			rigidbody.AddForce (boostForce, ForceMode.Impulse);
			powerLvl -= 0.1f;
			}
		}

		if (grounded) {
					rigidbody.AddForce (speed, ForceMode.Acceleration);

					if (Input.GetButtonDown ("Jump")) {
						rigidbody.AddForce (jumpForce, ForceMode.VelocityChange);
					}
		}

		if (!grounded & dblJump) {
							
			if (Input.GetButtonDown ("Jump")) {
				rigidbody.AddForce (jumpForce, ForceMode.VelocityChange);
				dblJump = false;
			}
			
		}
		
		if (transform.position.y < -5) {
		Application.LoadLevel(Application.loadedLevel);
		grounded = false;

		}
	 }
	void OnCollisionEnter(Collision prefab) {
				grounded = true;
				dblJump = true;
		}
	void OnCollisionExit(Collision prefab){
		grounded = false;
		}
	void OnTriggerEnter(Collider powerUp){
		Destroy(powerUp.gameObject);
		audio.Play();
		powerLvl += 1;
	}

}
