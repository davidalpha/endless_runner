using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public Vector3 speed = new Vector3 (5f, 0f, 0f);
	public Vector3 jumpForce = new Vector3 (0f, 5f, 0f);
	private bool grounded = false;
	private bool dblJump = true;

	void Start(){
	}

	void FixedUpdate() {
		if (Input.GetKey("space")){
			Debug.Log (rigidbody.velocity);
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
	}
	//this ray will generate a vector which points from the center of 
	//the falling object TO object hit. You subtract where you want to go from where you are
	
	var Ray MyRay = player.position - prefab.position;
	
	//this will declare a variable which will store information about the object hit
	RaycastHit MyRayHit; 
	
	//this is the actual raycast
	Physics.raycast(MyRay, out MyRayHit);
	
	//this will get the normal of the point hit, if you dont understand what a normal is 
	//wikipedia is your friend, its a simple idea, its a line which is tangent to a plane
	
	Vector3 MyNormal = MyRayHit.normal;
	
	//this will convert that normal from being relative to global axis to relative to an
	//objects local axis
	
	MyNormal = MyRayHit.transform.transformdirection(MyNormal);
	
	//this next line will compare the normal hit to the normals of each plane to find the 
	//side hit
	
	if(MyNormal == MyRayHit.transform.up)
	{
		grounded = true;
		dblJump = true;
	}
	//important note the use of the '-' sign this inverts the direction, -up == down. Down doesn't exist as a stored direction, you invert up to get it. 
	
	if(MyNormal == -MyRayHit.transform.up)
	{
	}
	if(MyNormal == MyRayHit.transform.right)
	{
		grounded = true;
		dblJump = true;
	}
	//note the '-' sign converting right to left
	if(MyNormal == -MyRayHit.transform.right)
	{
		grounded = true;
		dblJump = true;
	}

}
