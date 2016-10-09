using UnityEngine;
using System.Collections;

public class Cube4 : MonoBehaviour {

	Transform cubeTrans;
	Vector3 startPos, currentPos;
	public GameObject wallFront1, wallFront2, wallRight1, wallRight2;
	Animator animWallFront1, animWallFront2, animWallRight1, animWallRight2;

	

	// Use this for initialization
	void Start () {

	cubeTrans = gameObject.transform;
	startPos = cubeTrans.position;

	animWallFront1 = wallFront1.GetComponent<Animator>();
	animWallFront1.Play("Still");
	animWallFront2 = wallFront2.GetComponent<Animator>();
	animWallFront2.Play("Still");
	animWallRight1 = wallRight1.GetComponent<Animator>();
	animWallRight1.Play("Still");
	animWallRight2 = wallRight2.GetComponent<Animator>();
	animWallRight2.Play("Still");
	
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
		Function();
		currentPos = cubeTrans.position;
	}

	void Movement(){
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			cubeTrans.Translate(Vector3.left);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			cubeTrans.Translate(Vector3.right);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			cubeTrans.Translate(Vector3.forward);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			cubeTrans.Translate(Vector3.back);
		}
	}

	void Function(){

	if(currentPos.z > startPos.z){
		animWallFront1.Play("Take 001");
	}
	if(currentPos.z > startPos.z + 1){
		animWallFront2.Play("Take 002");
	}
	if(currentPos.x > startPos.x){
		animWallRight1.Play("Take 001");
	}
	if(currentPos.x > startPos.x + 1){
		animWallRight2.Play("Take 002");
	}
	}
}
