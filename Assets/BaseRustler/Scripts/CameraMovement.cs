using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    Vector3 mouseStart, touchStart;
 
	
	// Update is called once per frame
	void Update () {
        //DEV MOUSE CONTROLLER
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {

            /////
            if (mouseStart == Vector3.zero) {
                mouseStart = Input.mousePosition;
            }
            Vector3 mouseDelta = Input.mousePosition - mouseStart;
            mouseDelta = new Vector3(mouseDelta.x,0,mouseDelta.y);
            /////
            transform.Translate(mouseDelta * Time.deltaTime * Time.deltaTime);
        }
        else if (Input.GetMouseButtonUp(0)) {
            mouseStart = Vector3.zero;
        }
        //END DEV
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.deltaTime > 0.2f && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                if (touchStart == Vector3.zero) {
                    touchStart = touch.position;
                }
                Vector3 touchDelta = touchStart - (Vector3)touch.position;
                touchDelta = new Vector3(touchDelta.x, 0, touchDelta.y);
                transform.Translate(touchDelta * Time.deltaTime);
            }
        }
        if (Input.touchCount == 0) {
            touchStart = Vector3.zero;
        }
    }
}
