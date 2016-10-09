using UnityEngine;
using System.Collections;

public class Cube2 : MonoBehaviour {


	Transform cubeTrans;
	Vector3 startPos, currentPos;
	public GameObject roomForward, roomBack, roomUp, roomRight, roomLeft;
	Animator roomForwardAnim, roomBackAnim, roomUpAnim, roomRightAnim, roomLeftAnim;

	// Use this for initialization
	void Start () {
		cubeTrans = gameObject.GetComponent<Transform>();
		startPos = cubeTrans.position;
		roomForwardAnim = roomForward.GetComponent<Animator>();
		roomBackAnim = roomBack.GetComponent<Animator>();
		roomUpAnim = roomUp.GetComponent<Animator>();
		roomLeftAnim = roomLeft.GetComponent<Animator>();
		roomRightAnim = roomRight.GetComponent<Animator>();
		roomForwardAnim.Play("Still");
		roomBackAnim.Play("Still");
		roomUpAnim.Play("Still");
		roomLeftAnim.Play("Still");
		roomRightAnim.Play("Still");
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
		currentPos = cubeTrans.position;
		print(startPos);
		print(currentPos);


		if(Input.GetKeyDown(KeyCode.UpArrow)){
			roomForwardAnim.Play("Forward");
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			roomBackAnim.Play("Back");
		}if(Input.GetKeyDown(KeyCode.Space)){
			roomUpAnim.Play("Up");
		}if(Input.GetKeyDown(KeyCode.RightArrow)){
			roomLeftAnim.Play("Left");
		}if(Input.GetKeyDown(KeyCode.LeftArrow)){
			roomRightAnim.Play("Right");
		}
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
}
