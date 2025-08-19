using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_02 : MonoBehaviour {

//	public GameObject Wing_Right;
//	public GameObject Wing_Left;

	public Animation Wing;




	void OnTriggerEnter(Collider c) {
		
		if (c.tag.Equals("GameController")) {
			GetComponent<AudioSource> ().Play ();
			Wing ["door_02_wing"].speed = 1;
			Wing.Play ();
		}

	}

	void OnTriggerExit(Collider c) {

		if (c.tag.Equals("GameController")) {
			GetComponent<AudioSource> ().Play ();
			Wing ["door_02_wing"].time = Wing ["door_02_wing"].length;
			Wing ["door_02_wing"].speed = -1;
			Wing.Play ();
		}

	}


}
