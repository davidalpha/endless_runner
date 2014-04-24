﻿using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public Vector3 speed = new Vector3 (5f, 0f, 0f);
	public Vector3 accelGroundForce = new Vector3 (1f, 1f, 0f);
	public Vector3 jumpForce = new Vector3 (0f, 5f, 0f);
	public Vector3 boostForce = new Vector3 (0f, 0f, 5f);
	public float powerLvl = 0;

	private bool grounded = false;
	private bool dblJump = true;

	public GameObject particStars; //defined in inspector (ParticleStars)

	void Start(){
	}

	void FixedUpdate() {
		Debug.Log (particStars);
		if (powerLvl > 0 && Input.GetKey(KeyCode.X)){
			rigidbody.AddForce (boostForce, ForceMode.Impulse);
			powerLvl -= 0.1f;
			//Particles
			particStars.particleSystem.enableEmission = true;
			}
		else {
			//Particles
			particStars.particleSystem.enableEmission = false;
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


	void OnCollisionEnter(Collision collision) {
		grounded = true;
		dblJump = true;

		Debug.Log(collision.gameObject.name);
		
		if (collision.gameObject.name == "accelGround(Clone)") {
			rigidbody.AddForce (accelGroundForce, ForceMode.Impulse);
		}
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
