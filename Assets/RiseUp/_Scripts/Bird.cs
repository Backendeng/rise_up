using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	public Transform LeftWing;
	public Transform RightWing;

	// Use this for initialization
	void Start () {
		if (Random.Range(0,2) < 1) {
			LeftWing.GetComponent<MoveObstacle>().fallInPos = true;
			RightWing.GetComponent<MoveObstacle>().fallInPos = false;
		} else {
			LeftWing.GetComponent<MoveObstacle>().fallInPos = false;
			RightWing.GetComponent<MoveObstacle>().fallInPos = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
