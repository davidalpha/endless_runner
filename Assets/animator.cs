using UnityEngine;
using System.Collections;

public class animator : MonoBehaviour {

	public float animSpeed;

	Animator anim;
	GameObject pl;
	GameObject partic;

	// Use this for initialization
	void Start () {
		pl = GameObject.FindWithTag("Player");
		partic = GameObject.FindWithTag("Particles");
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//make Int to counter the micro fluctuations in Velocity.y due to collision platforms
		int roundedVelocityY = (int)Mathf.RoundToInt (pl.rigidbody.velocity.y);


		//Walking
		if (pl.rigidbody.velocity.x > 0 && roundedVelocityY == 0){
			anim.SetBool("PlayerWalk", true);
			anim.SetBool("PlayerIdle", false);
			anim.SetBool("PlayerJump", false);
			anim.SetBool("PlayerHurt", false);
		}
		//Jumping
		if (roundedVelocityY > 0){
			anim.SetBool("PlayerJump", true);
			anim.SetBool("PlayerWalk", false);
			anim.SetBool("PlayerIdle", false);
			anim.SetBool("PlayerHurt", false);

		}
		//Falling
		if (roundedVelocityY < 0){
			anim.SetBool("PlayerHurt", true);
			anim.SetBool("PlayerJump", false);
			anim.SetBool("PlayerWalk", false);
			anim.SetBool("PlayerIdle", false);

		}
		//Used for double jump
		if (Input.GetButtonDown ("Jump")) {
			anim.SetBool("PlayerWalk", true);
			anim.SetBool("PlayerIdle", false);
			anim.SetBool("PlayerJump", false);
			anim.SetBool("PlayerHurt", false);
		}

		//Set animation speed to velocity player
		anim.speed = pl.rigidbody.velocity.x * animSpeed/10;

		//Particle Controller
		if (roundedVelocityY > 6){
			//emit particles if jump power is high enoug (check velocity if jump has just started
			partic.particleSystem.enableEmission = true;
		}
		else {
			//reset particle system to prevent looping
			partic.particleSystem.enableEmission = false;
		}


	}
}
