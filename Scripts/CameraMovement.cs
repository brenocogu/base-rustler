using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    Vector3 mouseStart, touchStart;

    public Vector3 topZ, botZ, topX, botX;
	
	// Update is called once per frame
	void LateUpdate () {
        transform.Translate(MoveCamera(), Space.Self);
    }

    Vector3 MoveCamera() {
        #if UNITY_EDITOR
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {

            /////
            if (mouseStart == Vector3.zero)
            {
                mouseStart = Input.mousePosition;
            }
            Vector3 mouseDelta = Input.mousePosition - mouseStart;
            mouseDelta = CheckMoving(mouseDelta);
            mouseDelta = new Vector3(mouseDelta.x, 0, mouseDelta.y);
            /////
            return (mouseDelta * Time.deltaTime / 10);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseStart = Vector3.zero;
        }
        #endif
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (touchStart == Vector3.zero)
                {
                    touchStart = touch.position;
                }
                Vector3 touchDelta = touchStart - (Vector3)touch.position;
                touchDelta = CheckMoving(touchDelta);
                touchDelta = new Vector3(touchDelta.x, 0, touchDelta.y);
                return (touchDelta * Time.deltaTime / 10);
            }
        }
        if (Input.touchCount == 0)
        {
            touchStart = Vector3.zero;
        }

        return Vector3.zero;
    }

    Vector3 CheckMoving(Vector3 moved) {
        if ((transform.position.z > botZ.z && transform.position.z < topX.z && transform.position.x > botZ.x) && ((moved.y < 0 && moved.x > 0) || (moved.y > 0 && moved.x > 0)))
        {
            return Vector3.zero;
        }
        else if ((transform.position.x < botZ.x && transform.position.x > botX.x && transform.position.z < botZ.z) && ((moved.y < 0 && moved.x < 0) || (moved.y > 0 && moved.x < 0)))
        {
            return Vector3.zero;
        }
        else if ((transform.position.z > botX.z && transform.position.z < topZ.z && transform.position.x < botX.x) && ((moved.y > 0 && moved.x < 0) || (moved.y < 0 && moved.x < 0)))
        {
            return Vector3.zero;
        }
        else if ((transform.position.x > topZ.x && transform.position.x < topX.x && transform.position.z > topZ.z) && ((moved.y > 0 && moved.x > 0) || (moved.y > 0 && moved.x < 0)))
        {
            return Vector3.zero;
        }

        else
        {
            return moved;
        }
    }
}
