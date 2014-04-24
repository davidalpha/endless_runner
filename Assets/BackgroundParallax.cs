using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour {

	GameObject pl;
	Vector2 movement;

	// Use this for initialization
	void Start () {

		//Get player as gameobject
		pl = GameObject.FindWithTag("Player");

		//Set material to wrap texture UVs around
		renderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
	}
	
	// Update is called once per frame
	void Update () {
		//Get position x and y of player and translate to texture offset clouds with distance factor 200
		movement = pl.transform.position /200;
		renderer.material.mainTextureOffset = movement;

	}
}
