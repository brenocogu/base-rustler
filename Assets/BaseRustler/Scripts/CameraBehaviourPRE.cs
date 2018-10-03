using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourPRE : MonoBehaviour {

    public GameObject castle;

	float speed;

	void Start(){
		speed = 20;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 80;
		} else {
			speed = 20;
		}
        if (Input.GetKey(KeyCode.A)) {
			transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.W))
        {
			transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
			transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.Self);
        }

        if (Input.GetKey(KeyCode.E))
        {
			transform.Translate(new Vector3(0, speed* Time.deltaTime, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(new Vector3(0, -20 * Time.deltaTime, 0), Space.Self);
        }

        transform.LookAt(castle.transform.position);
    }
}
